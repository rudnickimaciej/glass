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
    using Translate.ViewModels.Helpers;

    internal sealed class Configuration2 : DbMigrationsConfiguration<ApplicationDbContext>
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
                "I enjoy my refuse-eye view of Venice and suggest to Roger that we make it the first of a Great Dustmen Of The World series, to be followed, if successful, by Great Sewers Of The World. Ron Passepartout baulks at this. 'I've just spent five weeks in the sewers, thank you!' He is referring not to conditions at TV Centre, but to a programme he's just made about a man who had taken refuge in the war in the sewers of Lvov. Ron has been everywhere and met everyone. On the very first day of filming the phone rang on location and a P.A., covering the mouthpiece, shouted, 'Ron! Can you do the Pope, Friday?'",
                "Moje dotychczasowe konto na fb zosta³o zhakowane zaatakowane przez hakera dlatego musia³am za³o¿yæ nowe. Pozdrowienia.Wszystko jak w tekœcie - muszê poinformowaæ moich znajomych a zale¿y mi ¿eby mnie zrozumiano poniewa¿ ja robiê b³êdy w czasach...niestety.",
                "Upon finding something important, a search party member was supposed to let somebody in charge know",
                "Yesterday's got nothin' for me.Some things could be better If we'd all just let them be"
        };


        public Configuration2()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

        }

        protected override void Seed(ApplicationDbContext context)
        {

            context.Votes.RemoveRange(context.Votes);
            context.Answers.RemoveRange(context.Answers);
            context.Questions.RemoveRange(context.Questions);
            context.SpamReports.RemoveRange(context.SpamReports);
            context.SpamReasons.RemoveRange(context.SpamReasons);


            //        Users
            //        var userStore = new UserStore<ApplicationUser>(context);
            //        var userManager = new UserManager<ApplicationUser>(userStore);

            //        var role = context.Roles.Where(r => r.Name == "Admin").FirstOrDefault();
            //        if(role!=null)
            //        {
            //            context.Roles.Add(new IdentityRole("Admin"));
            //            context.SaveChanges();
            //        }
            //        foreach (var user in userManager.Users)
            //        {
            //            context.Users.Remove(user);
            //        }

            //        context.SaveChanges();

            //        for (int i = 0; i < 10; i++)
            //        {
            //            var user = new ApplicationUser { UserName = "user_" + i, Description = _fakeContent[1], Email = "user_" + i + "@gmail.com", ProfileImageUrl = "/Content/images/users/" + "user_" + i };
            //            user.MemberSince = DateTime.Now;
            //            userManager.Create(user, "Mkjrq7o7o_");
            //        }

            //        context.SaveChanges();






            //        Questions 

            //        Random rnd = new Random();
            //        foreach(string c in _fakeContent)
            //        {
            //            for (int i = 0; i < 3; i++)
            //            {

            //                context.Questions.Add(
            //                    new Question
            //                    {
            //                        Created = DateTimeExtension.RandomDate(new DateTime(2020, 01, 01, 1, 30, 30), DateTime.Now),
            //                        Content = c,
            //                        User = userManager.Users.Where(u => u.Email == "user_" + i + "@gmail.com").FirstOrDefault()
            //                    }); ; ;

            //            }
            //        }



            //        context.SaveChanges();




            //        Answers

            //        foreach(Question q in context.Questions)
            //        {
            //            for(int i=0;i<2;i++)
            //            {
            //                context.Answers.Add(
            //                    new Answer
            //                    {
            //                        Content = _fakeContent[i],
            //                        Created = DateTime.Today,
            //                        Question = q,
            //                        User = userManager.Users.Where(u => u.Email == "user_"+i + "@gmail.com").FirstOrDefault()
            //                    });
            //            }
            //        }
            //        context.SaveChanges();

            //        Votes 
            //        foreach(Answer a in context.Answers)
            //        {
            //            for (int i=0;i<4;i++)
            //            {
            //                context.Votes.Add(
            //                    new Vote
            //                    {
            //                        Answer = a,
            //                        User = context.Users.Where(u => u.UserName == "user_" + i).FirstOrDefault(),
            //                        VoteType = VoteType.Positive
            //                    });
            //            }
            //        }
            //        context.SaveChanges();

            //        Spam Reason
            //        SpamReason r1 = new SpamReason("Wulgarny jêzyk.");
            //        SpamReason r2 = new SpamReason("Nic nie wnosz¹ca odpowiedŸ");
            //        SpamReason r3 = new SpamReason("B³êdny jêzyk t³umaczenia");
            //        SpamReason r4 = new SpamReason("Inne");

            //        context.SpamReasons.Add(r1);
            //        context.SpamReasons.Add(r2);
            //        context.SpamReasons.Add(r3);
            //        context.SpamReasons.Add(r4);

            //        context.SaveChanges();


            //        Spam Reports

            //        int x = 0;
            //        int y = -1;
            //        foreach(Answer a in context.Answers)
            //        {
            //            x++;
            //            if(x % 10==0) 
            //            {
            //                y++;
            //                if(y==4)
            //                {
            //                    y = 0;
            //                }
            //                context.SpamReports.Add(new SpamReport()
            //                {
            //                    Created = DateTime.Now,
            //                    ReportedAnswer = a,
            //                    ReportingUser = userManager.Users.Where(u => u.UserName.Equals("user_" +y)).FirstOrDefault(),
            //                    SpamReason = context.SpamReasons.ToArray()[y]
            //                });
            //            }
            //        }
            //        context.SaveChanges();
            //    }

            //    private VoteType getRandomVoteType(int x)
            //    {
            //        if (x % 2 == 0)
            //            return VoteType.Negative;
            //        else return VoteType.Positive;



            //    }
            //}
        }
    }
}
