using System;
using System.Collections.Generic;
using Nancy;

namespace NinjaGold{
    
    public class NinjaGoldModule : NancyModule{
        List<string> activityList;
        
        public NinjaGoldModule(){
            Get("/", args => {
                if(activityList == null){
                 activityList = new List<string>();
            }

                if(Request.Session["gold"] == null){
                Request.Session["gold"] = 0;
                }
                if(Request.Session["activity"] == null){
                    Request.Session["activity"] = new List<string>();
                    string starter = "Boop";
                    var addlist = new List<string>();
                    addlist.Add(starter);
                    Request.Session["activity"] = addlist;
                }
                // Console.WriteLine(activityList.Count);
                ViewBag.gold = Request.Session["gold"];
                List<string> listactivity = new List<string>();
                listactivity = Request.Session["activity"] as List<string>;
                foreach(var item in listactivity){
                    activityList.Add(item);
                }
                // Console.WriteLine(activityList[0]["message"]);
                return View["index.sshtml",activityList];
            });     
            Post("/proccess_money", args =>
            {
                Random rng = new Random();
                int gold = 0;
                int oldgold;
                switch((string)Request.Form.building){
                    case "farm":
                        gold = rng.Next(10,21);
                        oldgold = (int)Request.Session["gold"];
                        oldgold += gold;
                        Request.Session["gold"] = oldgold;
                        break;
                    case "cave":
                        gold = rng.Next(5,11);
                        oldgold = (int)Request.Session["gold"];
                        oldgold += gold;
                        Request.Session["gold"] = oldgold;
                        break;
                    case "house":
                        gold = rng.Next(2,6);
                        oldgold = (int)Request.Session["gold"];
                        oldgold += gold;
                        Request.Session["gold"] = oldgold;
                        break;
                    case "casino":
                        gold = rng.Next(0,101);
                        gold -= 50;
                        oldgold = (int)Request.Session["gold"];
                        oldgold += gold;
                        Request.Session["gold"] = oldgold;
                        break;
                }
                string activity;
                if(gold > 0){ 
                            // activity.Add("class","green");
                            activity = $"You earned {gold} gold from the {Request.Form.building}!";
                        }
                        else if (gold < 0){
                            // activity.Add("class","red");
                            activity = $"You lost {gold} gold from the {Request.Form.building}!";
                        } 
                        else { 
                            // activity.Add("class","none");
                            activity = $"You didn't make any gold from the {Request.Form.building}!";
                        }
                List<string> addlist = Request.Session["activity"] as List<string>;
                addlist.Add(activity);
                Request.Session["activity"] = addlist;
                return Response.AsRedirect("/");
            });
            Get("/reset", args => { 
                Request.Session["gold"] = null;
                Request.Session["activity"] = null;
                return Response.AsRedirect("/");
            });
        }
    }
}