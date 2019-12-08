namespace Translate.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Translate.Models;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Microsoft.AspNet.WebHooks.Utilities;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using Translate.Models.Domain;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        Random r = new Random();
        private struct Lang
        {
            public Lang(string name, string abbreviation)
            {

                Name = name;
                Abbreviation = abbreviation;
            }

            public string Name { get; set; }
            public string Abbreviation { get; set; }
        }

        private readonly IList<Lang> Languages = new ReadOnlyCollection<Lang>
            (
                new[]{
                    new Lang("polski","pl"),
                    new Lang( "angielski","eng"),
                    new Lang("hiszpañski","esp"),
                    new Lang( "francuski","fr"),
                    new Lang("w³oski","itl"),
                    new Lang( "german","ger"),
            });

        private readonly List<string> users = new List<string>
        {
            "user_0",
            "user_1",
            "user_3",
            "user_4",
            "user_5",
            "user_6",
            "user_7",
            "user_8",
            "user_9"
        };
        string[] _fakeTitles = new string[]
        {
                "Pytanie1: Buty, koty kolorowe okulary",
                "Pytanie2: Auto krêgle Warszawa, pomarañcza",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent tempus felis at erat?",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse ultrices metus eu elementum rhoncus. Suspendisse sodales luctus arcu et pulvinar. Quisque a?",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit.?"
        };

        string[] _fakeContent = new string[]
        {
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent tempus felis at erat vehicula eleifend. Maecenas sit amet ligula tellus. Suspendisse vestibulum diam vel felis?",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse ultrices metus eu elementum rhoncus. Suspendisse sodales luctus arcu et pulvinar. Quisque a?",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse faucibus quis nulla at lobortis. Sed ut augue rutrum mauris cursus posuere vitae?"
        };


        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

        }

        protected override void Seed(ApplicationDbContext context)
        {

            context.Votes.RemoveRange(context.Votes);
            context.Answers.RemoveRange(context.Answers);
            context.Questions.RemoveRange(context.Questions);
            context.Languages.RemoveRange(context.Languages);
            context.SpamReports.RemoveRange(context.SpamReports);
            context.SpamReasons.RemoveRange(context.SpamReasons);


            //Users
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var role = context.Roles.Where(r => r.Name == "Admin").FirstOrDefault();
            if(role!=null)
            {
                context.Roles.Add(new IdentityRole("Admin"));
                context.SaveChanges();
            }
            foreach (var user in userManager.Users)
            {
                context.Users.Remove(user);
            }

            context.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                var user = new ApplicationUser { UserName = "user_" + i, Description = _fakeContent[1], Email = "user_" + i + "@gmail.com", ProfileImageUrl = "/Content/images/users/" + "user_" + i };
                user.MemberSince = DateTime.Now;
                userManager.Create(user, "Mkjrq7o7o_");
            }

            context.SaveChanges();


            //Languages


            IEnumerable<Language> Langs = Languages.Select(l => new Language
            {

                Name = l.Name,
                Abbreviation = l.Abbreviation,
                Description = "opis",
                ImageUrl = "/Content/images/languages/" + l.Abbreviation + ".png"
            }); 

            context.Languages.AddRange(Langs);
    
            context.SaveChanges();


          

            //Questions 

            Random rnd = new Random();

                for (int i = 0; i < 2; i++)
                {
                    foreach(Lang lang in Languages)
                    {
                        foreach(Lang l in Languages)
                        {
                        context.Questions.Add(
                            new Question
                            {
                                Content = _fakeContent[i],
                                Created = DateTime.Today,
                                Title = _fakeTitles[i] ,

                                LanguageFrom =context.Languages.Where(language => language.Abbreviation == lang.Abbreviation).FirstOrDefault(),
                                LanguageTo = context.Languages.Where(language => language.Abbreviation == l.Abbreviation).FirstOrDefault(),
                                User = userManager.Users.Where(u => u.Email == "user_"+i + "@gmail.com").FirstOrDefault()
                            });;
                        }
                    }
                }


            context.SaveChanges();

            //Remove all the questions whose languages are the same f.e. eng=>eng 
            var questionsToRemove = context.Questions.Where(q => q.LanguageFrom == q.LanguageTo);
            context.Questions.RemoveRange(questionsToRemove);

            context.SaveChanges();



            //Answers

            foreach(Question q in context.Questions)
            {
                for(int i=0;i<2;i++)
                {
                    context.Answers.Add(
                        new Answer
                        {
                            Content = _fakeContent[i],
                            Created = DateTime.Today,
                            Question = q,
                            User = userManager.Users.Where(u => u.Email == "user_"+i + "@gmail.com").FirstOrDefault()
                        });
                }
            }
            context.SaveChanges();

            //Votes 
            foreach(Answer a in context.Answers)
            {
                for (int i=0;i<4;i++)
                {
                    context.Votes.Add(
                        new Vote
                        {
                            Answer = a,
                            User = context.Users.Where(u => u.UserName == "user_" + i).FirstOrDefault(),
                            VoteType = VoteType.Positive
                        });
                }
            }
            context.SaveChanges();

            //Spam Reason
            SpamReason r1 = new SpamReason("Wulgarny jêzyk.");
            SpamReason r2 = new SpamReason("Nic nie wnosz¹ca odpowiedŸ");
            SpamReason r3 = new SpamReason("B³êdny jêzyk t³umaczenia");
            SpamReason r4 = new SpamReason("Inne");

            context.SpamReasons.Add(r1);
            context.SpamReasons.Add(r2);
            context.SpamReasons.Add(r3);
            context.SpamReasons.Add(r4);

            context.SaveChanges();


            //Spam Reports

            int x = 0;
            int y = -1;
            foreach(Answer a in context.Answers)
            {
                x++;
                if(x % 10==0) 
                {
                    y++;
                    if(y==4)
                    {
                        y = 0;
                    }
                    context.SpamReports.Add(new SpamReport()
                    {
                        Created = DateTime.Now,
                        ReportedAnswer = a,
                        ReportingUser = userManager.Users.Where(u => u.UserName.Equals("user_" +y)).FirstOrDefault(),
                        SpamReason = context.SpamReasons.ToArray()[y]
                    });
                }
            }
            context.SaveChanges();
        }

        private VoteType getRandomVoteType(int x)
        {
            if (x % 2 == 0)
                return VoteType.Negative;
            else return VoteType.Positive;


            
        }
    }
}
