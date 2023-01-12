using FitnesCentar.Models.Enumeracija;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace FitnesCentar.Models
{
    public class Users
    {
        public Dictionary<string,User> listOfUsers;
        //public Dictionary<UlogaKorisnika, List<Korisnik>> listeKorisnika;
        //public List<FitnessCenter> fitnessCenters;
        public Dictionary<string, FitnessCenter> fitnessCenters;
        // public List<GrupniTrening> groupTrainings;
        public Dictionary<string, GroupTraining> groupTrainings;
        public Dictionary<string,List<string>> groupTrainingsVisitor; // <username,list treninga<name>> 
        public Dictionary<string,List<string>> groupTrainingsTrainer; // <korisnickoImeTrenera,list treninga<name,datumTreninga>>
        public Dictionary<string,List<string>> fitnessCentersAdmin; // <adminUsername,list naziva fitnes centara>
        public Dictionary<string,List<string>> visitorsGroupTraining; // <nazivGrupnogTreninga,list posetioca treninga>
        public Dictionary<string,List<Comment>> commentsOfFitnessCenter; // <fitnessCenterName,list komentara>
        //public Dictionary<string, List<Korisnik>> treneriVlasnika;
        //public Dictionary<string, string> treneriFitnesCentar; // <korisnickoImeTrenera,nazivfitnesCentra>
        //public Dictionary<Korisnik, FitnessCenter> treneriFitnesCentar;
        public List<Tuple<string, string>> trainerOfFitnessCenter; //<korisnickoImeTrenera,nazivfitnesCentra>
        

        //test
        public Users()
        {
            listOfUsers = new Dictionary<string, User>();
            groupTrainingsVisitor = new Dictionary<string, List<string>>();
            trainerOfFitnessCenter = new List<Tuple<string, string>>();
            fitnessCentersAdmin = new Dictionary<string, List<string>>();
            fitnessCenters = new Dictionary<string, FitnessCenter>();
            groupTrainings = new Dictionary<string, GroupTraining>();
            visitorsGroupTraining = new Dictionary<string, List<string>>();
            groupTrainingsTrainer = new Dictionary<string, List<string>>();
            commentsOfFitnessCenter = new Dictionary<string, List<Comment>>();   
        }
        public void SaveUserIntoDatabase(User u)
        {
            string path = "~/App_Data/Korisnici.txt";
            path = HostingEnvironment.MapPath(path);
            using (FileStream fileStream = new FileStream(path, FileMode.Append))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine($"{u.Username}?{u.Password}?{u.Naziv}?{u.Surname}?{u.Gender}?{u.Email}?{u.DateOfBirth}?{u.Role}?{u.Deleted}");
                    listOfUsers.Add(u.Username, u);
                }
            }
        }

        public void WriteLineToFile(string path, string content)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Append))
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    streamWriter.WriteLine(content);
        }

        public void WriteLinesToFile(string path, string[] content)
        {
            for (int i = 0; i < content.Length; i++)
                WriteLineToFile(path, content[i]);
        }

        public string ReadLineFromFile(string path)
        {
            string line = null;
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
                using (StreamReader streamReader = new StreamReader(fileStream))
                    line = streamReader.ReadLine();
            return line;
        }

        public string[] ReadLinesFromFile(string path, int length, bool breakForNull = false)
        {
            string[] lines = new string[length];
            for (int i = 0; i < length; i++)
            {
                lines[i] = ReadLineFromFile(path);
                if (breakForNull && lines[i] == null)
                    break;
            }
            return lines;
        }

        public void SaveFitnessCenterIntoDatabase(FitnessCenter fitnessCenter)
        {
            WriteLineToFile(
                HostingEnvironment.MapPath("~/App_Data/FitnessCenters.txt"),
                $"{fitnessCenter.Naziv}?{fitnessCenter.Adresa}?{fitnessCenter.DatumOtvaranja}?{fitnessCenter.AdminUsername}?{fitnessCenter.MonthlySubscription}?{fitnessCenter.YearlySubscription}?{fitnessCenter.PriceOfOneTraining}?{fitnessCenter.GroupTrainingPrice}?{fitnessCenter.PersonalTrainerPrice}?{fitnessCenter.Deleted}"
            );
            fitnessCenters.Add(fitnessCenter.Naziv, fitnessCenter);

            if (!fitnessCentersAdmin.ContainsKey(fitnessCenter.AdminUsername))
            {
                List<string> nevv = new List<string>();
                nevv.Add(fitnessCenter.Naziv);
                fitnessCentersAdmin.Add(fitnessCenter.AdminUsername, nevv);

                WriteLineToFile(
                    HostingEnvironment.MapPath("~/App_Data/FitnessCentersOfAdmin.txt"),
                    $"{fitnessCenter.AdminUsername}?{fitnessCenter.Naziv}"
                );
            }
            else
            {
                fitnessCentersAdmin[fitnessCenter.AdminUsername].Add(fitnessCenter.Naziv);

                string path = HostingEnvironment.MapPath("~/App_Data/FitnessCentersOfAdmin.txt");
                string[] data = ReadLinesFromFile(path, 200, true);
                
                for (int x = 0; x < data.Length; x++)
                {
                    if (data[x] == null)
                        break;

                    if (data[x].Contains(fitnessCenter.AdminUsername))
                    {
                        string temporary = "";
                        foreach(var nameOfFitnessCenter in fitnessCentersAdmin[fitnessCenter.AdminUsername])
                            temporary += $"?{nameOfFitnessCenter}";
                        data[x] = $"{fitnessCenter.AdminUsername}{temporary}";
                    }

                    WriteLineToFile(
                        HostingEnvironment.MapPath("~/App_Data/FitnessCentersOfAdmin.txt"),
                        data[x]
                    );
                }
            }
        }

        public void SaveTrainerFitnessCenterIntoDatabase(string Trainer,string FitnessCenter)
        {
            WriteLineToFile(HostingEnvironment.MapPath("~/App_Data/TrainerFitnessCenter.txt"), $"{Trainer}?{FitnessCenter}");
            trainerOfFitnessCenter.Add(Tuple.Create(Trainer, FitnessCenter));           
        }
        public void SaveGroupTrainingIntoDatabase(GroupTraining groupTraining,string Username)
        {
            WriteLineToFile(
                HostingEnvironment.MapPath("~/App_Data/GroupTrainings.txt"),
                $"{groupTraining.Naziv}?{groupTraining.Trening}?{groupTraining.FitnessCenterName}?{groupTraining.TrainingDuration}?{groupTraining.TrainingDate}?{groupTraining.MaximumNumberOfVisitors}?{groupTraining.Deleted}"
            );
            groupTrainings.Add(groupTraining.Naziv, groupTraining);

            if (groupTrainingsTrainer.ContainsKey(Username))
            {
                groupTrainingsTrainer[Username].Add(groupTraining.Naziv);

                var podaci = ReadLinesFromFile(HostingEnvironment.MapPath("~/App_Data/GroupTrainingsOfTrainer.txt"), 200,true);

                for (int x = 0; x < podaci.Length; x++)
                {
                    if (podaci[x] == null)
                    {
                        break;
                    }

                    if (podaci[x].Contains(Username))
                    {
                        string temporary = "";
                        foreach (var nameOfGroupTraining in groupTrainingsTrainer[Username])
                        {
                            temporary += $"?{nameOfGroupTraining}";
                        }
                        podaci[x] = $"{Username}{temporary}";
                    }

                    WriteLineToFile(HostingEnvironment.MapPath("~/App_Data/GroupTrainingsOfTrainer.txt"), podaci[x]);
                }
            }
            else
            {
                List<string> newList = new List<string>();
                newList.Add(groupTraining.Naziv);
                groupTrainingsTrainer.Add(Username, newList);

                WriteLineToFile(HostingEnvironment.MapPath("~/App_Data/GroupTrainingsOfTrainer.txt"), $"{Username}?{groupTraining.Naziv}");
            }
        }
        public void SaveCommentIntoDatabase(Comment comment)
        {
            string path = "~/App_Data/Komentari.txt";
            path = HostingEnvironment.MapPath(path);
            FileStream fileStream = new FileStream(path, FileMode.Append);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            //KorisnickoImePosetilac[0]?NameOfFitnessCenter[1]?Tekst[2]?Review[3]?Visibility[4]?
            streamWriter.WriteLine($"{comment.Username}?{comment.FitnessCenterName}?{comment.CommentText}?{comment.Review}?{comment.Visibility}");          
            if (commentsOfFitnessCenter.ContainsKey(comment.FitnessCenterName))
            {
                commentsOfFitnessCenter[comment.FitnessCenterName].Add(comment);
            }
            else
            {
                List<Comment> comments = new List<Comment>();
                comments.Add(comment);
                commentsOfFitnessCenter.Add(comment.FitnessCenterName, comments);
            }
            streamWriter.Close();
            fileStream.Close();
        }
        public User UserData(string userName)
        {
            User user = new User();
            listOfUsers.TryGetValue(userName,out user);
            return user;
        }
        public void ChangeUser(User newUser,User oldUser)
        {
            string path = "~/App_Data/Korisnici.txt";
            path = HostingEnvironment.MapPath(path);
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            List<string> lines = new List<string>();
            string temporary = "";

            while ((temporary = streamReader.ReadLine()) != null)
            {

                string[] podaci = temporary.Split('?');
                if (oldUser.Username != podaci[0])
                {
                    lines.Add(temporary);
                }

            }
            streamReader.Close();
            fileStream.Close();
            File.Delete(path);
            FileStream filestream = new FileStream(path, FileMode.Append, FileAccess.Write); 
            using (StreamWriter streamWriter = new StreamWriter(filestream))
            {
                foreach (var line in lines)
                {
                    streamWriter.WriteLine(line);
                }
                //Username[0]?Password[1]?Naziv[2]?Surname[3]?Gender[4]?Email[5]?Date[6]?Role[7]?Deleted(default false)[8]
                streamWriter.WriteLine($"{newUser.Username}?{newUser.Password}?{newUser.Naziv}?{newUser.Surname}?{newUser.Gender}?{newUser.Email}?{newUser.DateOfBirth}?{newUser.Role}?{newUser.Deleted}");
            }
            filestream.Close();

            listOfUsers[newUser.Username] = newUser;           
        }
        public void ChangeFitnessCenter(FitnessCenter newFitnessCenter)
        {
            string path = "~/App_Data/FitnessCenters.txt";
            path = HostingEnvironment.MapPath(path);
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            List<string> lines = new List<string>();
            string temporary = "";

            while ((temporary = streamReader.ReadLine()) != null)
            {

                string[] podaci = temporary.Split('?');
                if (newFitnessCenter.Naziv != podaci[0])
                {
                    lines.Add(temporary);
                }

            }
            streamReader.Close();
            fileStream.Close();
            File.Delete(path);
            FileStream filestream = new FileStream(path, FileMode.Append, FileAccess.Write);
            using (StreamWriter streamWriter = new StreamWriter(filestream))
            {
                foreach (var line in lines)
                {
                    streamWriter.WriteLine(line);
                }
                //Naziv[0]?Adresa[1]?DatumOtvaranja[2]?Vlasnik[3]?Mesecna[4]?Godisnja[5]?JedanT[6]?Grupni[7]?Personalni[8]?Deleted(default false)[9]
                streamWriter.WriteLine($"{newFitnessCenter.Naziv}?{newFitnessCenter.Adresa}?{newFitnessCenter.DatumOtvaranja}?{newFitnessCenter.AdminUsername}?{newFitnessCenter.MonthlySubscription}?{newFitnessCenter.YearlySubscription}?{newFitnessCenter.PriceOfOneTraining}?{newFitnessCenter.GroupTrainingPrice}?{newFitnessCenter.PersonalTrainerPrice}?{newFitnessCenter.Deleted}");
            }
            filestream.Close();

            fitnessCenters[newFitnessCenter.Naziv] = newFitnessCenter;        
        }
        public void ChangeGroupTraining(GroupTraining newGroupTraining)
        {
            string path = "~/App_Data/GroupTrainings.txt";
            path = HostingEnvironment.MapPath(path);
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            List<string> lines = new List<string>();
            string temporary = "";

            while ((temporary = streamReader.ReadLine()) != null)
            {

                string[] podaci = temporary.Split('?');
                if (newGroupTraining.Naziv != podaci[0])
                {
                    lines.Add(temporary);
                }

            }
            streamReader.Close();
            fileStream.Close();
            File.Delete(path);
            FileStream filestream = new FileStream(path, FileMode.Append, FileAccess.Write);
            using (StreamWriter streamWriter = new StreamWriter(filestream))
            {
                foreach (var line in lines)
                {
                    streamWriter.WriteLine(line);
                }
                //Naziv[0]?Trening[1]?NameOfFitnessCenter[2]?TrainingDuration[3]?TrainingDate[4]?MaxPosetioci[5]?Deleted(default false)[6]
                streamWriter.WriteLine($"{newGroupTraining.Naziv}?{newGroupTraining.Trening}?{newGroupTraining.FitnessCenterName}?{newGroupTraining.TrainingDuration}?{newGroupTraining.TrainingDate}?{newGroupTraining.MaximumNumberOfVisitors}?{newGroupTraining.Deleted}");
            }
            filestream.Close();
            groupTrainings[newGroupTraining.Naziv] = newGroupTraining;            
        }
        public void ChangeComment(Comment comment, string Username,string Visibility)
        {
            string comm = $"{comment.Username}?{comment.FitnessCenterName}?{comment.CommentText}?{comment.Review}?{comment.Visibility}";
            string path = "~/App_Data/Komentari.txt";
            path = HostingEnvironment.MapPath(path);
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            List<string> lines = new List<string>();
            string temporary = "";

            while ((temporary = streamReader.ReadLine()) != null)
            {

                string[] podaci = temporary.Split('?');
                if (comm != temporary)
                {
                    lines.Add(temporary);
                }

            }
            streamReader.Close();
            fileStream.Close();
            File.Delete(path);
            FileStream filestream = new FileStream(path, FileMode.Append, FileAccess.Write);
            using (StreamWriter streamWriter = new StreamWriter(filestream))
            {
                foreach (var line in lines)
                {
                    streamWriter.WriteLine(line);
                }
                //Naziv[0]?Adresa[1]?DatumOtvaranja[2]?Vlasnik[3]?Mesecna[4]?Godisnja[5]?JedanT[6]?Grupni[7]?Personalni[8]?Deleted(default false)[9]
                streamWriter.WriteLine($"{comment.Username}?{comment.FitnessCenterName}?{comment.CommentText}?{comment.Review}?{Visibility}");
            }
            filestream.Close();
            

            if (commentsOfFitnessCenter.ContainsKey(comment.FitnessCenterName))
            {
                foreach(var oldComment in commentsOfFitnessCenter[comment.FitnessCenterName])
                {
                    if(oldComment.CommentText == comment.CommentText && oldComment.Username == comment.Username && oldComment.FitnessCenterName == comment.FitnessCenterName)
                    {
                        oldComment.Visibility = Visibility;
                    }
                }
            }
        }
        public void BlockTrainer(string Username)
        {
            string path = "~/App_Data/Korisnici.txt";
            path = HostingEnvironment.MapPath(path);
            StreamReader streamReader = new StreamReader(path);
            string[] podaci = new string[200];
            
            for (int x = 0; x < podaci.Length; x++)
            {
                podaci[x] = streamReader.ReadLine();
                if (podaci[x] == null)
                {
                    break;
                }

            }
            streamReader.Close();
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                for (int x = 0; x < podaci.Length; x++)
                {
                    if (podaci[x] == null)
                    {
                        break;
                    }

                    if (podaci[x].Contains(Username))
                    {
                        podaci[x] = podaci[x].Replace("False", "True");
                        //podaci[x].Remove(x);
                    }
                    streamWriter.WriteLine(podaci[x]);
                }
                streamWriter.Close();
            }
            listOfUsers.Remove(Username);
            path = "~/App_Data/TrainerFitnessCenter.txt";
            path = HostingEnvironment.MapPath(path);
            streamReader = new StreamReader(path);
            podaci = new string[200];

            for (int x = 0; x < podaci.Length; x++)
            {
                podaci[x] = streamReader.ReadLine();
                if (podaci[x] == null)
                {
                    break;
                }

            }
            streamReader.Close();
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                for (int x = 0; x < podaci.Length; x++)
                {
                    if (podaci[x] == null)
                    {
                        break;
                    }

                    if (podaci[x].Contains(Username))
                    {
                        podaci[x].Remove(x);
                    }
                    else 
                    {
                        streamWriter.WriteLine(podaci[x]);
                    }                  
                }
                streamWriter.Close();
            }
            trainerOfFitnessCenter.Remove(trainerOfFitnessCenter.Find(x => x.Item1.Equals(Username)));
        }
        public void DeleteFitnessCenter(string nameOfFitnessCenter,string Username)
        {
            List<Tuple<string,string>> trainerFitnessCenter = new List<Tuple<string, string>>();
            List<string> deletedTrainers = new List<string>();
            string path = "~/App_Data/FitnessCenters.txt";
            path = HostingEnvironment.MapPath(path);
            StreamReader streamReader = new StreamReader(path);
            string[] podaci = new string[200];
            
            for (int x = 0; x < podaci.Length; x++)
            {
                podaci[x] = streamReader.ReadLine();
                if (podaci[x] == null)
                {
                    break;
                }

            }
            streamReader.Close();
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                for (int x = 0; x < podaci.Length; x++)
                {
                    if (podaci[x] == null)
                    {
                        break;
                    }

                    if (podaci[x].Contains(nameOfFitnessCenter))
                    {
                        podaci[x] = podaci[x].Replace("False", "True");                       
                    }
                    streamWriter.WriteLine(podaci[x]);
                }
                streamWriter.Close();
            }
            fitnessCenters.Remove(nameOfFitnessCenter);




            path = "~/App_Data/TrainerFitnessCenter.txt";
            path = HostingEnvironment.MapPath(path);
            //FileStream fileStream = new FileStream(path, FileMode.Open);
            streamReader = new StreamReader(path);
            podaci = new string[200];
            List<string> lines = new List<string>();
            string temporary = "";

            while ((temporary = streamReader.ReadLine()) != null)
            {

                podaci = temporary.Split('?');
                if (nameOfFitnessCenter!= podaci[1])
                {
                    lines.Add(temporary);
                }

            }
            streamReader.Close();
            //fileStream.Close();
            File.Delete(path);
            FileStream filestream = new FileStream(path, FileMode.Append, FileAccess.Write);
            using (StreamWriter streamWriter = new StreamWriter(filestream))
            {
                foreach (var line in lines)
                {
                    streamWriter.WriteLine(line);
                }
                streamWriter.Close();
            }
            filestream.Close();
            foreach (var tuplovi in trainerOfFitnessCenter)
            {
                if(tuplovi.Item2 == nameOfFitnessCenter)
                {
                    path = "~/App_Data/Korisnici.txt";
                    path = HostingEnvironment.MapPath(path);
                    streamReader = new StreamReader(path);
                    podaci = new string[200];

                    for (int x = 0; x < podaci.Length; x++)
                    {
                        podaci[x] = streamReader.ReadLine();
                        if (podaci[x] == null)
                        {
                            break;
                        }

                    }
                    streamReader.Close();
                    using (StreamWriter streamWriter = new StreamWriter(path))
                    {
                        for (int x = 0; x < podaci.Length; x++)
                        {
                            if (podaci[x] == null)
                            {
                                break;
                            }

                            if (podaci[x].Contains(tuplovi.Item1))
                            {
                                podaci[x] = podaci[x].Replace("False", "True");
                                deletedTrainers.Add(tuplovi.Item1);
                                trainerFitnessCenter.Add(tuplovi);
                                
                            }
                            streamWriter.WriteLine(podaci[x]);
                        }
                        streamWriter.Close();
                    }
                    listOfUsers.Remove(tuplovi.Item1);
                    
                }
            }
            foreach(var t in trainerFitnessCenter)
            {
                if (trainerOfFitnessCenter.Contains(t))
                {
                    trainerOfFitnessCenter.Remove(t);
                }
            }




            path = "~/App_Data/FitnessCentersOfAdmin.txt";
            path = HostingEnvironment.MapPath(path);
            streamReader = new StreamReader(path);
            podaci = new string[200];

            for (int x = 0; x < podaci.Length; x++)
            {
                podaci[x] = streamReader.ReadLine();
                if (podaci[x] == null)
                {
                    break;
                }

            }
            streamReader.Close();
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                for (int x = 0; x < podaci.Length; x++)
                {
                    if (podaci[x] == null)
                    {
                        break;
                    }

                    if (podaci[x].Contains(nameOfFitnessCenter))
                    {
                        temporary = "";
                        fitnessCentersAdmin[Username].Remove(nameOfFitnessCenter);
                        foreach (var fitnessCenter in fitnessCentersAdmin[Username])
                        {
                            temporary += $"?{fitnessCenter}";
                        }
                        streamWriter.WriteLine($"{Username}{temporary}");
                        
                    }
                    else
                    {
                        streamWriter.WriteLine(podaci[x]);
                    }
                }
                streamWriter.Close();
            }
            
        }
        public void DeleteGroupTraining(string nameOfGroupTraining,string Username)
        {
            string path = "~/App_Data/GroupTrainings.txt";
            path = HostingEnvironment.MapPath(path);
            StreamReader streamReader = new StreamReader(path);
            string[] podaci = new string[200];

            for (int x = 0; x < podaci.Length; x++)
            {
                podaci[x] = streamReader.ReadLine();
                if (podaci[x] == null)
                {
                    break;
                }

            }
            streamReader.Close();
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                for (int x = 0; x < podaci.Length; x++)
                {
                    if (podaci[x] == null)
                    {
                        break;
                    }

                    if (podaci[x].Contains(nameOfGroupTraining))
                    {
                        podaci[x] = podaci[x].Replace("False", "True");
                    }
                    streamWriter.WriteLine(podaci[x]);
                }
                streamWriter.Close();
            }
            groupTrainings.Remove(nameOfGroupTraining);


            path = "~/App_Data/GroupTrainingsOfTrainer.txt";
            path = HostingEnvironment.MapPath(path);
            streamReader = new StreamReader(path);
            podaci = new string[200];
            string temporary = "";

            for (int x = 0; x < podaci.Length; x++)
            {
                podaci[x] = streamReader.ReadLine();
                if (podaci[x] == null)
                {
                    break;
                }

            }
            streamReader.Close();
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                for (int x = 0; x < podaci.Length; x++)
                {
                    if (podaci[x] == null)
                    {
                        break;
                    }

                    if (podaci[x].Contains(nameOfGroupTraining))
                    {
                        temporary = "";
                        groupTrainingsTrainer[Username].Remove(nameOfGroupTraining);
                        foreach (var fitnessCenter in groupTrainingsTrainer[Username])
                        {
                            temporary += $"?{fitnessCenter}";
                        }
                        streamWriter.WriteLine($"{Username}{temporary}");

                    }
                    else
                    {
                        streamWriter.WriteLine(podaci[x]);
                    }
                }
                streamWriter.Close();
            }

        }
        public void ListOfUsers()
        {
            //List<Korisnik> users = new List<Korisnik>();

            string path = "~/App_Data/Korisnici.txt";
            path = HostingEnvironment.MapPath(path);

            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            string temporary = "";
            while ((temporary = streamReader.ReadLine()) != null)
            {
                string[] podaci = temporary.Split('?');
                User user = new User();
                if (podaci[8] == "False")
                {
                    //Username[0]?Password[1]?Naziv[2]?Surname[3]?Gender[4]?Email[5]?DateOfBirth[6]?Role[7]?Deleted[8]
                    user.Username = podaci[0];
                    user.Password = podaci[1];
                    user.Naziv = podaci[2];
                    user.Surname = podaci[3];
                    user.Gender = podaci[4];
                    user.Email = podaci[5];
                    user.DateOfBirth = podaci[6];
                    if(podaci[7] == "VISITOR")
                    {
                        user.Role = UserRole.VISITOR;
                    }
                    else if (podaci[7] == "TRAINER")
                    {
                        user.Role = UserRole.TRAINER;
                    }
                    else if (podaci[7] == "ADMIN")
                    {
                        user.Role = UserRole.ADMIN;
                    }
                    
                    if (podaci[8] == "False")
                    {
                        user.Deleted = false;
                        listOfUsers.Add(user.Username, user);
                    }
                    else
                    {
                        user.Deleted = true;
                    }
                }
            }
            streamReader.Close();
            fileStream.Close();          
        }
        public void ListOfFitnessCenters()
        {
            string path = "~/App_Data/FitnessCenters.txt";
            path = HostingEnvironment.MapPath(path);

            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            string temporary = "";
            while ((temporary = streamReader.ReadLine()) != null)
            {
                string[] podaci = temporary.Split('?');
                FitnessCenter fitnessCenter = new FitnessCenter();
                
                //Naziv[0]?Adresa[1]?DatumOtvaranja[2]?AdminUsername[3]?Mesecna[4]?Godisnja[5]?JedanTrening[6]?Grupni[7]?Personalni[8]?Deleted[9]?
                fitnessCenter.Naziv = podaci[0];
                fitnessCenter.Adresa = podaci[1];
                fitnessCenter.DatumOtvaranja =  Int32.Parse(podaci[2]);
                fitnessCenter.AdminUsername = podaci[3];
                fitnessCenter.MonthlySubscription = Int32.Parse(podaci[4]);
                fitnessCenter.YearlySubscription = Int32.Parse(podaci[5]);
                fitnessCenter.PriceOfOneTraining = Int32.Parse(podaci[6]);
                fitnessCenter.GroupTrainingPrice = Int32.Parse(podaci[7]);
                fitnessCenter.PersonalTrainerPrice = Int32.Parse(podaci[8]);

                if (podaci[9] == "False")
                {
                    fitnessCenter.Deleted = false;
                    fitnessCenters.Add(fitnessCenter.Naziv, fitnessCenter);
                }
                else
                {
                    fitnessCenter.Deleted = true;
                }
                
            }
            streamReader.Close();
            fileStream.Close();
        }
        public void ListaGrupnihTreninga()
        {
            string path = "~/App_Data/GroupTrainings.txt";
            path = HostingEnvironment.MapPath(path);

            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            string temporary = "";
            while ((temporary = streamReader.ReadLine()) != null)
            {
                string[] podaci = temporary.Split('?');
                GroupTraining groupTraining = new GroupTraining();

                //Naziv[0]?TypeOfTraining[1]?FitnessCenter[2]TrainingDuration[3]?TrainingDate[4]?MaximumNumberOfVisitors[5]?BrojPrijavljenih[6]?Deleted[7]
                groupTraining.Naziv = podaci[0];
                if (podaci[1] == "YOGA")
                {
                    groupTraining.Trening = TrainingType.YOGA;
                }
                else if (podaci[1] == "BODYPUMP")
                {
                    groupTraining.Trening = TrainingType.BODYPUMP;
                }
                else
                {
                    groupTraining.Trening = TrainingType.LESMILLSTONE;
                }
                
                groupTraining.FitnessCenterName = podaci[2];
                groupTraining.TrainingDuration = podaci[3];
                groupTraining.TrainingDate = podaci[4];
                groupTraining.MaximumNumberOfVisitors = Int32.Parse(podaci[5]);
               

                if (podaci[6] == "False")
                {
                    groupTraining.Deleted = false;
                    groupTrainings.Add(groupTraining.Naziv, groupTraining);
                }
                else
                {
                    groupTraining.Deleted = true;
                }

            }
            streamReader.Close();
            fileStream.Close();
        }
        public void ListOfComments()
        {
            string path = "~/App_Data/Komentari.txt";
            path = HostingEnvironment.MapPath(path);

            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            string temporary = "";
            while ((temporary = streamReader.ReadLine()) != null)
            {
                string[] podaci = temporary.Split('?');
                Comment k = new Comment();

                //KorisnickoImePosetilac[0]?nameOfFitnessCenter[1]?Tekst[2]Review[3]?Visibility[4]
                k.Username = podaci[0];
                k.FitnessCenterName = podaci[1];
                k.CommentText = podaci[2];
                k.Review = Int32.Parse(podaci[3]);
                k.Visibility = podaci[4];
                if (commentsOfFitnessCenter.ContainsKey(k.FitnessCenterName))
                {
                    commentsOfFitnessCenter[k.FitnessCenterName].Add(k);
                }
                else
                {
                    List<Comment> comments = new List<Comment>();
                    comments.Add(k);
                    commentsOfFitnessCenter.Add(k.FitnessCenterName, comments);
                }

            }
            streamReader.Close();
            fileStream.Close();
        }
        public List<GroupTraining> GroupTrainingsOfVisitors(string Username)
        {
            //List<string> list = new List<string>();
            List<GroupTraining> trainings = new List<GroupTraining>();
            if (groupTrainingsVisitor.ContainsKey(Username))
            {
                //list = groupTrainingsVisitor[Username];
                foreach(var nameOfGroupTraining in groupTrainingsVisitor[Username])
                {
                    trainings.Add(groupTrainings[nameOfGroupTraining]);
                }
            }         
            return trainings;
        }
        public string TrainerFitnessCenter(string Username)
        {
            foreach(var tr in trainerOfFitnessCenter)
            {
                if(tr.Item1 == Username)
                {
                    if(tr.Item2 != "")
                    {
                        return tr.Item2;
                    }
                    else
                    {
                        return "Still don't work";
                    }
                    
                }
            }
            return "Trainer does not exist";
        }
        public void GroupTrainingsOfTrainer()
        {
            string path = "~/App_Data/GroupTrainingsOfTrainer.txt";
            path = HostingEnvironment.MapPath(path);

            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            string temporary = "";
            while ((temporary = streamReader.ReadLine()) != null)
            {
                List<string> trainings = new List<string>();
                string[] podaci = temporary.Split('?');

                //Username[0]?[name][1]?
                for (int x = 1; x < podaci.Length; ++x)
                {
                    trainings.Add(podaci[x]);
                }
                groupTrainingsTrainer.Add(podaci[0], trainings);

            }
            streamReader.Close();
            fileStream.Close();
        }
        public void VisitorsOfGroupTraining()
        {
            string path = "~/App_Data/VisitorsOfGroupTraining.txt";
            path = HostingEnvironment.MapPath(path);

            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            string temporary = "";
            while ((temporary = streamReader.ReadLine()) != null)
            {
                List<string> users = new List<string>();
                string[] podaci = temporary.Split('?');

                //name[0]?[Username][1]?
                for (int x = 1; x < podaci.Length; ++x)
                {
                    users.Add(podaci[x]);
                }
                visitorsGroupTraining.Add(podaci[0], users);

            }
            streamReader.Close();
            fileStream.Close();
        }
        public List<string> FitnessCentersOfAdmin(string Username)
        {
            List<string> fitnessCenters = new List<string>();
            fitnessCentersAdmin.TryGetValue(Username, out fitnessCenters);
            return fitnessCenters;
        }
        public List<FitnessCenter> FitnessCentersOfAdmin2(string Username)
        {
            List<FitnessCenter> fitnessCenter = new List<FitnessCenter>();
            foreach(var fitnesCentar in fitnessCenters.Values)
            {
                if(fitnesCentar.AdminUsername == Username)
                {
                    fitnessCenter.Add(fitnesCentar);
                }
            }
            return fitnessCenter;
        }
        public List<GroupTraining> GroupTrainingsOfFitnessCenter(string FitnessCenterName)
        {
            List<GroupTraining> groupTraining = new List<GroupTraining>();
            foreach(var training in groupTrainings.Values)
            {
                if(training.FitnessCenterName == FitnessCenterName)
                {
                    groupTraining.Add(training);
                }
            }
            return groupTraining;
        }
        public List<GroupTraining> GroupTrainingsOfTrainer(string Username)
        {
            List<GroupTraining> groupTraining = new List<GroupTraining>();
            if (groupTrainingsTrainer.ContainsKey(Username))
            {
                foreach (var s in groupTrainingsTrainer[Username])
                {
                    foreach (var grupniTrening in groupTrainings.Values)
                    {
                        if (grupniTrening.Naziv == s)
                        {
                            groupTraining.Add(grupniTrening);
                            break;
                        }
                    }
                }
            }           
            return groupTraining;
        }
        public List<User> TrainersOfAdmins(string Username)
        {
            List<User> trainers = new List<User>();
            User trainer = new User();
            List<Tuple<string, string>> tuplovi = new List<Tuple<string, string>>();
            List<string> fitnessCenter = new List<string>();
            fitnessCentersAdmin.TryGetValue(Username, out fitnessCenter);
            if (fitnessCenter != null)
            {
                foreach (var item in fitnessCenter)
                {
                    foreach(var t in trainerOfFitnessCenter)
                    {
                        if(t.Item2 == item)
                        {
                            listOfUsers.TryGetValue(t.Item1, out trainer);
                            trainers.Add(trainer);
                        }
                    }
                }
            }
            
            return trainers;
        }
        public List<FitnessCenter> FitnesCenterSorting(string typeOfSorting,string sortBy)
        {
            List<FitnessCenter> fitnessCenter = new List<FitnessCenter>();           
            fitnessCenter = fitnessCenters.Values.ToList();
            if (typeOfSorting == "Rastuće")
            {
                if(sortBy == "Naziv")
                {
                    fitnessCenter.Sort((x, y) => x.Naziv.CompareTo(y.Naziv));
                }
                else if(sortBy == "Adresa")
                {
                    //fitnessCenter.Sort((x, y) => x.Adresa.Split(',')[1].CompareTo(y.Adresa.Split(',')[1]));
                    fitnessCenter.Sort((x, y) => x.Adresa.CompareTo(y.Adresa));
                }
                else if(sortBy == "DatumOtvaranja")
                {
                    fitnessCenter.Sort((x, y) => x.DatumOtvaranja.CompareTo(y.DatumOtvaranja));
                }
               
            }
            else if(typeOfSorting == "Opadajuće")
            {
               
                if (sortBy == "Naziv")
                {
                    fitnessCenter.Sort((x, y) => y.Naziv.CompareTo(x.Naziv));
                }
                else if (sortBy == "Adresa")
                {
                   //fitnessCenter.Sort((x, y) => y.Adresa.Split(',')[1].CompareTo(x.Adresa.Split(',')[1]));
                   fitnessCenter.Sort((x, y) => y.Adresa.CompareTo(x.Adresa));
                }
                else if (sortBy == "DatumOtvaranja")
                {
                    fitnessCenter.Sort((x, y) => y.DatumOtvaranja.CompareTo(x.DatumOtvaranja));
                }
            }
            else
            {
                fitnessCenter.Sort((x, y) => x.Naziv.CompareTo(y.Naziv));
            }
            
            return fitnessCenter;
        } 
        public List<FitnessCenter> FitnessCenterSearch(string Naziv,string Adresa,string DonjaGranica,string GornjaGranica)
        {
            List<FitnessCenter> centers = new List<FitnessCenter>();
            List<FitnessCenter> newFitnessCenter = new List<FitnessCenter>();
            centers = fitnessCenters.Values.ToList();

            
            foreach(var fitnessCenter in centers)
            {
                if (!string.IsNullOrEmpty(Naziv))
                {
                    if(!(fitnessCenter.Naziv == Naziv))
                    {
                        continue;
                    }
                }
                if (!string.IsNullOrEmpty(Adresa))
                {
                    if (!(fitnessCenter.Adresa.Contains(Adresa)))
                    {
                        continue;
                    }
                }
                if (!string.IsNullOrEmpty(DonjaGranica))
                {
                    if ((fitnessCenter.DatumOtvaranja < Int32.Parse(DonjaGranica)))
                    {
                        continue;
                    }
                }
                if (!string.IsNullOrEmpty(GornjaGranica))
                {
                    if ((fitnessCenter.DatumOtvaranja > Int32.Parse(GornjaGranica)))
                    {
                        continue;
                    }
                }
                newFitnessCenter.Add(fitnessCenter);
            }
          
            return newFitnessCenter;
        }
        public List<GroupTraining> GroupTrainingSorting(string typeOfSorting, string sortBy, string Username)
        {
            List<GroupTraining> groupTraining = new List<GroupTraining>();
            if (listOfUsers[Username].Role == UserRole.VISITOR)
            {
                groupTraining = GroupTrainingsOfVisitors(Username);
            }
            else
            {
                groupTraining = GroupTrainingsOfTrainer(Username);
            }
            
            if (typeOfSorting == "Rastuće")
            {
                if (sortBy == "Naziv")
                {
                    groupTraining.Sort((x, y) => x.Naziv.CompareTo(y.Naziv));
                }
                else if (sortBy == "TypeOfTraining")
                {
                    
                    groupTraining.Sort((x, y) => x.Trening.ToString().CompareTo(y.Trening.ToString()));
                }
                else if (sortBy == "DateOfMaintenance")
                {
                    groupTraining.Sort((x, y) => x.TrainingDate.CompareTo(y.TrainingDate));
                }

            }
            else if (typeOfSorting == "Opadajuće")
            {

                if (sortBy == "Naziv")
                {
                    groupTraining.Sort((x, y) => y.Naziv.CompareTo(x.Naziv));
                }
                else if (sortBy == "TypeOfTraining")
                {
                    groupTraining.Sort((x, y) => y.Trening.ToString().CompareTo(x.Trening.ToString()));
                }
                else if (sortBy == "DateOfMaintenance")
                {
                    groupTraining.Sort((x, y) => y.TrainingDate.CompareTo(x.TrainingDate));
                }
            }
            else
            {
                groupTraining.Sort((x, y) => x.Naziv.CompareTo(y.Naziv));
            }

            return groupTraining;
        }
        public List<GroupTraining> GroupTrainingSearch(string Naziv, string TypeOfTraining, string DonjaGranica, string GornjaGranica,string Username)
        {
            List<GroupTraining> trainings = new List<GroupTraining>();
            List<GroupTraining> newGroupTraining = new List<GroupTraining>();

            if (listOfUsers[Username].Role == UserRole.VISITOR)
            {
                trainings = GroupTrainingsOfVisitors(Username);
            }
            else
            {
                trainings = GroupTrainingsOfTrainer(Username);
            }

            foreach (var groupTraining in trainings)
            {
                if (!string.IsNullOrEmpty(Naziv))
                {
                    if (!(groupTraining.Naziv == Naziv))
                    {
                        continue;
                    }
                }
                if (!string.IsNullOrEmpty(TypeOfTraining))
                {
                    if (!(groupTraining.Trening.ToString() == TypeOfTraining))
                    {
                        continue;
                    }
                }
                if (!string.IsNullOrEmpty(DonjaGranica))
                {
                    if ((DateTime.Parse(groupTraining.TrainingDate) < DateTime.Parse(DonjaGranica)))
                    {
                        continue;
                    }
                }
                if (!string.IsNullOrEmpty(GornjaGranica))
                {
                    if ((DateTime.Parse(groupTraining.TrainingDate) > DateTime.Parse(GornjaGranica)))
                    {
                        continue;
                    }
                }
                newGroupTraining.Add(groupTraining);
            }

            return newGroupTraining;
        }
        public void FitnessCentersOfAdmin()
        {   
            string path = "~/App_Data/FitnessCentersOfAdmin.txt";
            path = HostingEnvironment.MapPath(path);

            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            string temporary = "";
            while ((temporary = streamReader.ReadLine()) != null)
            {
                List<string> centers = new List<string>();
                string[] podaci = temporary.Split('?');              
                
                //Username[0]?[name,datumTreninga][1]?
                for (int x = 1; x < podaci.Length; ++x)
                {
                    centers.Add(podaci[x]);
                }
                fitnessCentersAdmin.Add(podaci[0], centers);
               
            }
            streamReader.Close();
            fileStream.Close();
        }
        public void GroupTrainingsOfVisitors()
        {
            string path = "~/App_Data/GroupTrainingsOfVisitors.txt";
            path = HostingEnvironment.MapPath(path);

            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            string temporary = "";
            while ((temporary = streamReader.ReadLine()) != null)
            {
                string[] podaci = temporary.Split('?');
               // string[] data2;
                List<string> trainings = new List<string>();
                for (int x = 1; x < podaci.Length; ++x)
                {
                    //data2 = podaci[x].Split(',');
                    trainings.Add(podaci[x]);
                    //groupTrainingsVisitor[podaci[0]].Add(podaci[x]);
                }
                groupTrainingsVisitor.Add(podaci[0], trainings);

            }
            streamReader.Close();
            fileStream.Close();
        }
        public void TrainerFitnessCenter()
        {
            string path = "~/App_Data/TrainerFitnessCenter.txt";
            path = HostingEnvironment.MapPath(path);

            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            string temporary = "";
            while ((temporary = streamReader.ReadLine()) != null)
            {
                string[] podaci = temporary.Split('?');              
                //Trainer[0]?FitnessCenter[1]
                trainerOfFitnessCenter.Add(Tuple.Create(podaci[0], podaci[1]));
            }
            streamReader.Close();
            fileStream.Close();
        }

        public List<Comment> ListOfComments(string nameOfFitnessCenter)
        {
            if (commentsOfFitnessCenter.ContainsKey(nameOfFitnessCenter))
            {
                return commentsOfFitnessCenter[nameOfFitnessCenter]
;           }
            return new List<Comment>();
        }
        public List<Comment> ListOfCommentsAdmin(string Username)
        {
            List<Comment> list = new List<Comment>();
            foreach (var fitnessCenter in fitnessCentersAdmin[Username])
            {
                if (commentsOfFitnessCenter.ContainsKey(fitnessCenter))
                {
                    foreach(var k in commentsOfFitnessCenter[fitnessCenter])
                    {
                        list.Add(k);
                    }
                }
            }
            return list;
        }
        public void AddGroupTrainingVisitor(string Username,string NameOfGroupTraining)
        {
            string path = "~/App_Data/GroupTrainingsOfVisitors.txt";
            path = HostingEnvironment.MapPath(path);
          
            //StreamWriter streamWriter = new StreamWriter(fileStream);         

            if (groupTrainingsVisitor.ContainsKey(Username))
            {
                groupTrainingsVisitor[Username].Add(NameOfGroupTraining);
                FileStream fileStream = new FileStream(path, FileMode.Open);
                StreamReader streamReader = new StreamReader(fileStream);
                string[] podaci = new string[200];

                for (int x = 0; x < podaci.Length; x++)
                {
                    podaci[x] = streamReader.ReadLine();
                    if (podaci[x] == null)
                    {
                        break;
                    }

                }
                streamReader.Close();
                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    for (int x = 0; x < podaci.Length; x++)
                    {
                        if (podaci[x] == null)
                        {
                            break;
                        }

                        if (podaci[x].Contains(Username))
                        {
                            string temporary = "";
                            foreach (var nameOfGroupTraining in groupTrainingsVisitor[Username])
                            {
                                temporary += $"?{nameOfGroupTraining}";
                            }
                            podaci[x] = $"{Username}{temporary}";
                        }
                        streamWriter.WriteLine(podaci[x]);
                    }
                    streamWriter.Close();
                }

            }
            else
            {
                List<string> newList = new List<string>();
                newList.Add(NameOfGroupTraining);
                groupTrainingsVisitor.Add(Username, newList);
                FileStream fileStream = new FileStream(path, FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                //Username[0]?Naziv[1]
                streamWriter.WriteLine($"{Username}?{NameOfGroupTraining}");

                streamWriter.Close();
                fileStream.Close();
            }

            if (visitorsGroupTraining.ContainsKey(NameOfGroupTraining))
            {
                visitorsGroupTraining[NameOfGroupTraining].Add(Username);
                path = "~/App_Data/VisitorsOfGroupTraining.txt";
                path = HostingEnvironment.MapPath(path);
                FileStream fileStream = new FileStream(path, FileMode.Open);
                StreamReader streamReader = new StreamReader(fileStream);
                string[] podaci = new string[200];

                for (int x = 0; x < podaci.Length; x++)
                {
                    podaci[x] = streamReader.ReadLine();
                    if (podaci[x] == null)
                    {
                        break;
                    }

                }
                streamReader.Close();
                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    for (int x = 0; x < podaci.Length; x++)
                    {
                        if (podaci[x] == null)
                        {
                            break;
                        }

                        if (podaci[x].Contains(NameOfGroupTraining))
                        {
                            string temporary = "";
                            foreach (var ime in visitorsGroupTraining[NameOfGroupTraining])
                            {
                                temporary += $"?{ime}";
                            }
                            podaci[x] = $"{NameOfGroupTraining}{temporary}";
                        }
                        streamWriter.WriteLine(podaci[x]);
                    }
                    streamWriter.Close();
                }

            }
            else
            {
                List<string> newList = new List<string>();
                newList.Add(Username);
                visitorsGroupTraining.Add(NameOfGroupTraining, newList);
                path = "~/App_Data/VisitorsOfGroupTraining.txt";
                path = HostingEnvironment.MapPath(path);
                FileStream fileStream = new FileStream(path, FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                //Username[0]?Naziv[1]
                streamWriter.WriteLine($"{NameOfGroupTraining}?{Username}");

                streamWriter.Close();
                fileStream.Close();
            }

        }
        public bool TrainerLoginCheck(string Username,string NameOfGroupTraining)
        {
            //maksimalno jednom da se prijavi x provera da li je prijavljen maksimalan broj ucesnika
            if (visitorsGroupTraining.ContainsKey(NameOfGroupTraining))
            {
                if (visitorsGroupTraining[NameOfGroupTraining].Count() == groupTrainings[NameOfGroupTraining].MaximumNumberOfVisitors)
                {
                    return true;
                }
            }

            if (groupTrainingsVisitor.ContainsKey(Username))
            {
                if (groupTrainingsVisitor[Username].Contains(NameOfGroupTraining))
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckVisitorComment(string Visitor,string NameOfFitnessCenter)
        {
            List<GroupTraining> list = new List<GroupTraining>();
            list = GroupTrainingsOfFitnessCenter(NameOfFitnessCenter);
            foreach (var nameOfGroupTraining in groupTrainingsVisitor[Visitor])
            {
                foreach(var tr in list)
                {
                    if(tr.Naziv == nameOfGroupTraining)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool CheckFutureTrainingsFitnessCenter(string NameOfFitnessCenter)
        {
            List<GroupTraining> groupTraining = new List<GroupTraining>();
            groupTraining = GroupTrainingsOfFitnessCenter(NameOfFitnessCenter);
            foreach (var training in groupTraining)
            {
                if(training.FitnessCenterName == NameOfFitnessCenter)
                {
                    if(DateTime.Parse(training.TrainingDate) > DateTime.Now)
                    {
                        return true;
                    }
                    
                }
            }
            return false;
        }
        public bool UserNameCheck(string userName)
        {
            return listOfUsers.ContainsKey(userName);
        }
        public bool ValidationOfDocumment(string Date)
        {
            DateTime result;
            return DateTime.TryParse(Date, out result);

        }
        public bool LoggedUserChrck(string username,string password)
        {
            return listOfUsers.Where(x => x.Value.Username == username && x.Value.Password == password).Count() > 0;
        }
        public string RoleCheck(string Username)
        {
            foreach (var user in listOfUsers.Values)
            {
                if(user.Username == Username)
                {
                    return user.Role.ToString();
                }
               
            }
            return "";
        }

    }
}