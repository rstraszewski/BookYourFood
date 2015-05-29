using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using ReservationDomain.Model;
using System.Linq;
using System;
using Common.Model;

namespace Database
{
    public class ByfDbInitializer : DropCreateDatabaseIfModelChanges<ByfDbContext>
    {
        protected override void Seed(ByfDbContext context)
        {
            context.Roles.Add(new IdentityRole("User"));
            context.Roles.Add(new IdentityRole("Administrator"));
            context.Roles.Add(new IdentityRole("Restaurant"));

            #region Filling up ingredients' table
            var ingredients = new List<Ingredient>();
            #region Ingredient 1 veal
            var tmpIngredient1 = new Ingredient()
            {
                Name = "veal",
                Description = "veal meat",
                IngredientType = IngredientType.Meat,
                IngredientImportance = IngredientImportance.Main,
                HashTags = new List<HashTag>()
            };
            AddHashTagToIngredient("veal", tmpIngredient1, context);
            AddHashTagToIngredient("meat", tmpIngredient1, context);
            AddHashTagToIngredient("casual", tmpIngredient1, context);
            AddHashTagToIngredient("normal", tmpIngredient1, context);
            ingredients.Add(tmpIngredient1);
            #endregion
            #region Ingredient 2 backed potatoes
            var tmpIngredient2 = new Ingredient()
            {
                Name = "backed potatoes",
                //Description = "backed potatoes",
                IngredientType = IngredientType.Other,
                IngredientImportance = IngredientImportance.Main,
                HashTags = new List<HashTag>()
            };
            AddHashTagToIngredient("vegetable", tmpIngredient2, context);
            AddHashTagToIngredient("potatoe", tmpIngredient2, context);
            AddHashTagToIngredient("casual", tmpIngredient2, context);
            AddHashTagToIngredient("normal", tmpIngredient2, context);
            AddHashTagToIngredient("supplement", tmpIngredient2, context);
            AddHashTagToIngredient("main_course", tmpIngredient2, context);
            ingredients.Add(tmpIngredient2);
            #endregion
            #region Ingredient 3 coleslaw salad
            var tmpIngredient3 = new Ingredient()
            {
                Name = "coleslaw salad",
                //Description = "veal meat",
                IngredientType = IngredientType.Other,
                IngredientImportance = IngredientImportance.Secondary,
                HashTags = new List<HashTag>()
            };
            AddHashTagToIngredient("salad", tmpIngredient3, context);
            AddHashTagToIngredient("polish", tmpIngredient3, context);
            AddHashTagToIngredient("casual", tmpIngredient3, context);
            AddHashTagToIngredient("normal", tmpIngredient3, context);
            AddHashTagToIngredient("vegetable", tmpIngredient3, context);
            ingredients.Add(tmpIngredient3);
            #endregion
            #region Ingredient 4 pork meat
            var tmpIngredient4 = new Ingredient()
            {
                Name = "pork meat",
                Description = "pork meat",
                IngredientType = IngredientType.Meat,
                IngredientImportance = IngredientImportance.Main,
                HashTags = new List<HashTag>()
            };
            AddHashTagToIngredient("pork", tmpIngredient4, context);
            AddHashTagToIngredient("meat", tmpIngredient4, context);
            AddHashTagToIngredient("casual", tmpIngredient4, context);
            AddHashTagToIngredient("normal", tmpIngredient4, context);
            ingredients.Add(tmpIngredient4);
            #endregion
            #region Ingredient 5 beef
            var tmpIngredient5 = new Ingredient()
            {
                Name = "beef",
                Description = "beef meat",
                IngredientType = IngredientType.Meat,
                IngredientImportance = IngredientImportance.Main,
                HashTags = new List<HashTag>()
            };
            AddHashTagToIngredient("beef", tmpIngredient5, context);
            AddHashTagToIngredient("meat", tmpIngredient5, context);
            AddHashTagToIngredient("casual", tmpIngredient5, context);
            AddHashTagToIngredient("normal", tmpIngredient5, context);
            ingredients.Add(tmpIngredient5);
            #endregion
            #region Ingredient 6 ribs
            var tmpIngredient6 = new Ingredient()
            {
                Name = "ribs",
                Description = "ribs",
                IngredientType = IngredientType.Meat,
                IngredientImportance = IngredientImportance.Main,
                HashTags = new List<HashTag>()
            };
            AddHashTagToIngredient("ribs", tmpIngredient6, context);
            AddHashTagToIngredient("meat", tmpIngredient6, context);
            AddHashTagToIngredient("sophisticated", tmpIngredient6, context);
            ingredients.Add(tmpIngredient6);
            #endregion
            #region Ingredient 7 parmesan
            var tmpIngredient7 = new Ingredient()
            {
                Name = "parmesan",
                Description = "parmesan cheese",
                IngredientType = IngredientType.Other,
                IngredientImportance = IngredientImportance.Main,
                HashTags = new List<HashTag>()
            };
            AddHashTagToIngredient("cheese", tmpIngredient7, context);
            AddHashTagToIngredient("parmesan", tmpIngredient7, context);
            AddHashTagToIngredient("sophisticated", tmpIngredient7, context);
            ingredients.Add(tmpIngredient7);
            #endregion
            #region Ingredient 8 pork knuckle
            var tmpIngredient8 = new Ingredient()
            {
                Name = "pork knuckle",
                Description = "pork knuckle",
                IngredientType = IngredientType.Meat,
                IngredientImportance = IngredientImportance.Main,
                HashTags = new List<HashTag>()
            };
            AddHashTagToIngredient("pork", tmpIngredient8, context);
            AddHashTagToIngredient("knuckle", tmpIngredient8, context);
            AddHashTagToIngredient("meat", tmpIngredient8, context);
            AddHashTagToIngredient("casual", tmpIngredient8, context);
            AddHashTagToIngredient("normal", tmpIngredient8, context);
            AddHashTagToIngredient("fat", tmpIngredient8, context);
            ingredients.Add(tmpIngredient8);
            #endregion
            #region Ingredient 9 rice
            var tmpIngredient9 = new Ingredient()
            {
                Name = "rice",
                Description = "rice",
                IngredientType = IngredientType.Other,
                IngredientImportance = IngredientImportance.Main,
                HashTags = new List<HashTag>()
            };
            AddHashTagToIngredient("rice", tmpIngredient9, context);
            AddHashTagToIngredient("chinese", tmpIngredient9, context);
            AddHashTagToIngredient("casual", tmpIngredient9, context);
            AddHashTagToIngredient("normal", tmpIngredient9, context);
            ingredients.Add(tmpIngredient9);
            #endregion
            #region Ingredient 10 pasta
            var tmpIngredient10 = new Ingredient()
            {
                Name = "pasta",
                Description = "pasta",
                IngredientType = IngredientType.Other,
                IngredientImportance = IngredientImportance.Main,
                HashTags = new List<HashTag>()
            };
            AddHashTagToIngredient("pasta", tmpIngredient10, context);
            AddHashTagToIngredient("casual", tmpIngredient10, context);
            AddHashTagToIngredient("normal", tmpIngredient10, context);
            ingredients.Add(tmpIngredient10);
            #endregion            

            foreach(var i in ingredients)
            {
                context.Ingredients.Add(i);
            }
            context.SaveChanges();
            #endregion

            #region Filling up meals' table
            var meals = new List<Meal>();
            #region Meal 1 veal cutlet with baked potatoes and salad
            var mealTmp1 = new Meal()
            {
                Name = "veal cutlet with baked potatoes and salad",
                Description = @"A standard polish constructional worker's meal....",
                Price = 12.00M,
                HashTags = new List<HashTag>(),
                Ingredients = new List<Ingredient>()
            };
            AddHashTagToMeal("cutlet", mealTmp1, context);
            AddHashTagToMeal("veal", mealTmp1, context);
            AddHashTagToMeal("polish", mealTmp1, context);
            AddHashTagToMeal("casual", mealTmp1, context);
            AddHashTagToMeal("normal", mealTmp1, context);
            AddHashTagToMeal("meat", mealTmp1, context);
            AddHashTagToMeal("dinner", mealTmp1, context);
            AddHashTagToMeal("main_course", mealTmp1, context);
            AddHashTagToMeal("potatoe", mealTmp1, context);
            AddHashTagToMeal("complete", mealTmp1, context);
            AddIngredientToMeal("veal", mealTmp1, context);
            AddIngredientToMeal("backed potatoes", mealTmp1, context);
            AddIngredientToMeal("coleslaw salad", mealTmp1, context);
            meals.Add(mealTmp1);
            #endregion
            #region Meal 2 pork cutlet with baked potatoes and salad
            var mealTmp2 = new Meal()
            {
                Name = "pork cutlet with baked potatoes and salad",
                Description = @"A standard polish constructional worker's meal....",
                Price = 12.00M,
                HashTags = new List<HashTag>(),
                Ingredients = new List<Ingredient>()
            };
            AddHashTagToMeal("cutlet", mealTmp2, context);
            AddHashTagToMeal("pork", mealTmp2, context);
            AddHashTagToMeal("polish", mealTmp2, context);
            AddHashTagToMeal("casual", mealTmp2, context);
            AddHashTagToMeal("normal", mealTmp2, context);
            AddHashTagToMeal("meat", mealTmp2, context);
            AddHashTagToMeal("complete", mealTmp1, context);
            AddHashTagToMeal("dinner", mealTmp2, context);
            AddHashTagToMeal("main_course", mealTmp2, context);
            AddHashTagToMeal("potatoe", mealTmp2, context);
            AddIngredientToMeal("pork meat", mealTmp2, context);
            AddIngredientToMeal("backed potatoes", mealTmp2, context);
            AddIngredientToMeal("coleslaw salad", mealTmp2, context);
            meals.Add(mealTmp2);
            #endregion
            #region Meal 3 beef steak with baked potatoes and salad
            var mealTmp3 = new Meal()
            {
                Name = "beef steak with baked potatoes and salad",
                Description = @"A standard polish constructional worker's meal....",
                Price = 12.00M,
                HashTags = new List<HashTag>(),
                Ingredients = new List<Ingredient>()
            };
            AddHashTagToMeal("cutlet", mealTmp3, context);
            AddHashTagToMeal("beef", mealTmp3, context);
            AddHashTagToMeal("steak", mealTmp3, context);
            AddHashTagToMeal("american", mealTmp3, context);
            AddHashTagToMeal("casual", mealTmp3, context);
            AddHashTagToMeal("complete", mealTmp1, context);
            AddHashTagToMeal("normal", mealTmp3, context);
            AddHashTagToMeal("meat", mealTmp3, context);
            AddHashTagToMeal("dinner", mealTmp3, context);
            AddHashTagToMeal("main_course", mealTmp3, context);
            AddHashTagToMeal("potatoe", mealTmp3, context);
            AddIngredientToMeal("beef", mealTmp3, context);
            AddIngredientToMeal("backed potatoes", mealTmp3, context);
            AddIngredientToMeal("coleslaw salad", mealTmp3, context);
            meals.Add(mealTmp3);
            #endregion
            #region Meal 4 ribs in country style
            var mealTmp4 = new Meal()
            {
                Name = "ribs in country style",
                Description = @"A standard polish constructional worker's meal....",
                Price = 12.00M,
                HashTags = new List<HashTag>(),
                Ingredients = new List<Ingredient>()
            };
            AddHashTagToMeal("rib", mealTmp4, context);
            AddHashTagToMeal("polish", mealTmp4, context);
            AddHashTagToMeal("german", mealTmp4, context);
            AddHashTagToMeal("sophisticated", mealTmp4, context);
            AddHashTagToMeal("meat", mealTmp4, context);
            AddHashTagToMeal("dinner", mealTmp4, context);
            AddHashTagToMeal("main_course", mealTmp4, context);
            AddIngredientToMeal("ribs", mealTmp4, context);
            meals.Add(mealTmp4);
            #endregion
            #region Meal 5 pork knuckle
            var mealTmp5 = new Meal()
            {
                Name = "pork knuckle",
                Description = @"A standard polish constructional worker's meal....",
                Price = 12.00M,
                HashTags = new List<HashTag>(),
                Ingredients = new List<Ingredient>()
            };
            AddHashTagToMeal("pork", mealTmp5, context);
            AddHashTagToMeal("knuckle", mealTmp5, context);
            AddHashTagToMeal("polish", mealTmp5, context);
            AddHashTagToMeal("german", mealTmp5, context);
            AddHashTagToMeal("filling", mealTmp5, context);
            AddHashTagToMeal("meat", mealTmp5, context);
            AddHashTagToMeal("dinner", mealTmp5, context);
            AddHashTagToMeal("supper", mealTmp5, context);
            AddHashTagToMeal("main_course", mealTmp5, context);
            AddIngredientToMeal("pork knuckle", mealTmp5, context);
            meals.Add(mealTmp5);
            #endregion
            #region Meal 6 stuffed cabbage rolls
            var mealTmp6 = new Meal()
            {
                Name = "stuffed cabbage rolls",
                Description = @"A standard polish constructional worker's meal....",
                Price = 12.00M,
                HashTags = new List<HashTag>(),
                Ingredients = new List<Ingredient>()
            };
            AddHashTagToMeal("cabbage", mealTmp6, context);
            AddHashTagToMeal("rice", mealTmp6, context);
            AddHashTagToMeal("polish", mealTmp6, context);
            AddHashTagToMeal("german", mealTmp6, context);
            AddHashTagToMeal("meat", mealTmp6, context);
            AddHashTagToMeal("dinner", mealTmp6, context);
            AddHashTagToMeal("supper", mealTmp6, context);
            AddHashTagToMeal("main_course", mealTmp6, context);
            AddIngredientToMeal("rice", mealTmp6, context);
            AddIngredientToMeal("pork meat", mealTmp6, context);            
            meals.Add(mealTmp6);
            #endregion
            #region Meal 7 spaghetti
            var mealTmp7 = new Meal()
            {
                Name = "spaghetti",
                Description = @"Italiano",
                Price = 12.00M,
                HashTags = new List<HashTag>(),
                Ingredients = new List<Ingredient>()
            };
            AddHashTagToMeal("spaghetti", mealTmp7, context);
            AddHashTagToMeal("pasta", mealTmp7, context);
            AddHashTagToMeal("italian", mealTmp7, context);
            AddHashTagToMeal("parmesan", mealTmp7, context);
            AddHashTagToMeal("meat", mealTmp7, context);
            AddHashTagToMeal("dinner", mealTmp7, context);
            AddHashTagToMeal("supper", mealTmp7, context);
            AddHashTagToMeal("main_course", mealTmp7, context);
            AddHashTagToMeal("tomatoe", mealTmp7, context);
            AddHashTagToMeal("sauce", mealTmp7, context);
            AddHashTagToMeal("romantic", mealTmp7, context);
            AddHashTagToMeal("basil", mealTmp7, context);
            AddIngredientToMeal("pasta", mealTmp7, context);
            AddIngredientToMeal("parmesan", mealTmp7, context);
            AddIngredientToMeal("pork meat", mealTmp7, context);
            meals.Add(mealTmp7);
            #endregion
            #region Meal 8 dumplings with meat
            var mealTmp8 = new Meal()
            {
                Name = "dumplings",
                Description = @"dumplings with meat",
                Price = 12.00M,
                HashTags = new List<HashTag>(),
                Ingredients = new List<Ingredient>()
            };
            AddHashTagToMeal("dumpling", mealTmp8, context);
            AddHashTagToMeal("meat", mealTmp8, context);
            AddHashTagToMeal("polish", mealTmp8, context);
            AddHashTagToMeal("casual", mealTmp8, context);
            AddHashTagToMeal("dinner", mealTmp8, context);
            AddHashTagToMeal("main_course", mealTmp8, context);
            AddHashTagToMeal("normal", mealTmp8, context);
            AddIngredientToMeal("pork meat", mealTmp8, context);
            meals.Add(mealTmp8);
            #endregion
            #region Meal 9 cheese and potato dumplings
            var mealTmp9 = new Meal()
            {
                Name = "cheese and potato dumplings",
                Description = @"cheese and potato dumplings",
                Price = 12.00M,
                HashTags = new List<HashTag>(),
                Ingredients = new List<Ingredient>()
            };
            AddHashTagToMeal("dumplings", mealTmp9, context);
            AddHashTagToMeal("potatoe", mealTmp9, context);
            AddHashTagToMeal("polish", mealTmp9, context);
            AddHashTagToMeal("casual", mealTmp9, context);
            AddHashTagToMeal("dinner", mealTmp9, context);
            AddHashTagToMeal("main_course", mealTmp9, context);
            AddHashTagToMeal("normal", mealTmp9, context);
            meals.Add(mealTmp9);
            #endregion
            #region Meal 10 potato pancakes
            var mealTmp10 = new Meal()
            {
                Name = "potato pancakes",
                Description = @"potato pancakes",
                Price = 12.00M,
                HashTags = new List<HashTag>(),
                Ingredients = new List<Ingredient>()
            };
            AddHashTagToMeal("pancake", mealTmp10, context);
            AddHashTagToMeal("potatoe", mealTmp10, context);
            AddHashTagToMeal("polish", mealTmp10, context);
            AddHashTagToMeal("casual", mealTmp10, context);
            AddHashTagToMeal("dinner", mealTmp10, context);
            AddHashTagToMeal("main_course", mealTmp10, context);
            AddHashTagToMeal("normal", mealTmp10, context);
            AddHashTagToMeal("vegetarian", mealTmp10, context);
            meals.Add(mealTmp10);
            #endregion

            foreach (var m in meals)
            {
                context.Meals.Add(m);
            }
            context.SaveChanges();
            #endregion

            #region Filling up drinks' table
            var drinks = new List<Drink>();
            #region Drink 1 Red wine, sweet
            var tmpDrink1 = new Drink()
            {
                Name = "Red wine",
                DrinkType = DrinkType.Wine,
                HashTags = new List<HashTag>()
            };
            AddHashTagToDrink("red",tmpDrink1,context);
            AddHashTagToDrink("wine", tmpDrink1, context);
            AddHashTagToDrink("sophisticated", tmpDrink1, context);
            AddHashTagToDrink("sweet", tmpDrink1, context);
            AddHashTagToDrink("alcohol", tmpDrink1, context);
            drinks.Add(tmpDrink1);
            #endregion
            #region Drink 2 White wine, sweet
            var tmpDrink2 = new Drink()
            {
                Name = "White wine",
                DrinkType = DrinkType.Wine,
                HashTags = new List<HashTag>()
            };
            AddHashTagToDrink("white", tmpDrink2, context);
            AddHashTagToDrink("wine", tmpDrink2, context);
            AddHashTagToDrink("sophisticated", tmpDrink2, context);
            AddHashTagToDrink("sweet", tmpDrink2, context);
            AddHashTagToDrink("alcohol", tmpDrink2, context);
            drinks.Add(tmpDrink2);
            #endregion
            #region Drink 3 Heineken
            var tmpDrink3 = new Drink()
            {
                Name = "Heineken",
                DrinkType = DrinkType.Beer,
                HashTags = new List<HashTag>()
            };
            AddHashTagToDrink("beer", tmpDrink3, context);
            AddHashTagToDrink("lager", tmpDrink3, context);
            AddHashTagToDrink("alcohol", tmpDrink3, context);
            AddHashTagToDrink("pale", tmpDrink3, context);
            drinks.Add(tmpDrink3);
            #endregion
            #region Drink 4 Ksiazece ciemne
            var tmpDrink4 = new Drink()
            {
                Name = "Książęce ciemne",
                DrinkType = DrinkType.Beer,
                HashTags = new List<HashTag>()
            };
            AddHashTagToDrink("beer", tmpDrink4, context);
            AddHashTagToDrink("bock", tmpDrink4, context);
            AddHashTagToDrink("alcohol", tmpDrink4, context);
            AddHashTagToDrink("polish", tmpDrink4, context);
            drinks.Add(tmpDrink4);
            #endregion
            #region Drink 5 Orange juice
            var tmpDrink5 = new Drink()
            {
                Name = "Orange juice",
                DrinkType = DrinkType.Juice,
                HashTags = new List<HashTag>()
            };
            AddHashTagToDrink("juice", tmpDrink5, context);
            AddHashTagToDrink("orange", tmpDrink5, context);
            AddHashTagToDrink("fruit", tmpDrink5, context);
            AddHashTagToDrink("fresh", tmpDrink5, context);
            drinks.Add(tmpDrink5);
            #endregion
            #region Drink 6 Sparkling water
            var tmpDrink6 = new Drink()
            {
                Name = "Sparkling water",
                DrinkType = DrinkType.Other,
                HashTags = new List<HashTag>()
            };
            AddHashTagToDrink("wather", tmpDrink6, context);
            AddHashTagToDrink("normal", tmpDrink6, context);
            AddHashTagToDrink("neutral", tmpDrink6, context);
            AddHashTagToDrink("sparkling", tmpDrink6, context);
            AddHashTagToDrink("soda", tmpDrink6, context);
            drinks.Add(tmpDrink6);
            #endregion

            foreach(var d in drinks)
            {
                context.Drinks.Add(d);
            }
            context.SaveChanges();
            #endregion

            #region Filling up questions table
            var questions = new List<Question>();
            Answer ans1, ans2, ans3, ans4, ans5, ans6, ans7, ans8, ans9;
            Question question;
            #region Are you a vegetarian?
            question = new Question() { Value = "Do you like meat?", Answers = new List<Answer>() };
            ans1 = new Answer() { Value = "Yes", HashTags = new List<HashTag>() };
            ans2 = new Answer() { Value = "No", HashTags = new List<HashTag>() };
            AddHashTagToAnswer("meat", ans1, context); //yes
            AddHashTagToAnswer("vegetarian", ans2, context); //no
            question.Answers.Add(ans1);
            question.Answers.Add(ans2);
            questions.Add(question);
            #endregion
            #region Do you like sophisticated food?
            question = new Question() { Value = "Do you like sophisticated food?", Answers = new List<Answer>() };
            ans1 = new Answer() { Value = "Yes", HashTags = new List<HashTag>() };
            ans2 = new Answer() { Value = "No", HashTags = new List<HashTag>() };
            AddHashTagToAnswer("sophisticated", ans1, context); //yes
            AddHashTagToAnswer("wine", ans1, context); //yes
            AddHashTagToAnswer("casual", ans2, context); //no
            AddHashTagToAnswer("normal", ans2, context); //no
            AddHashTagToAnswer("neutral", ans2, context); //no
            AddHashTagToAnswer("beer", ans2, context); //no
            question.Answers.Add(ans1);
            question.Answers.Add(ans2);
            questions.Add(question);
            #endregion
            #region What kind of cuisine do you prefer?
            question = new Question() { Value = "What kind of cuisine do you prefer?", Answers = new List<Answer>() };
            ans1 = new Answer() { Value = "Polish", HashTags = new List<HashTag>() };
            ans2 = new Answer() { Value = "German", HashTags = new List<HashTag>() };
            ans3 = new Answer() { Value = "Italian", HashTags = new List<HashTag>() };
            ans4 = new Answer() { Value = "American", HashTags = new List<HashTag>() };
            ans5 = new Answer() { Value = "Chinese", HashTags = new List<HashTag>() };
            AddHashTagToAnswer("polish", ans1, context);
            AddHashTagToAnswer("german", ans2, context);
            AddHashTagToAnswer("italian", ans3, context);
            AddHashTagToAnswer("american", ans4, context);
            AddHashTagToAnswer("chinese", ans5, context);
            question.Answers.Add(ans1);
            question.Answers.Add(ans2);
            question.Answers.Add(ans3);
            question.Answers.Add(ans4);
            question.Answers.Add(ans5);
            questions.Add(question);
            #endregion
            #region What kind of food do you prefer?
            question = new Question() { Value = "What kind of food do you prefer?", Answers = new List<Answer>() };
            ans1 = new Answer() { Value = "Spicy", HashTags = new List<HashTag>() };
            ans2 = new Answer() { Value = "Sweet and sour", HashTags = new List<HashTag>() };
            ans3 = new Answer() { Value = "Sweet", HashTags = new List<HashTag>() };
            ans4 = new Answer() { Value = "Sour", HashTags = new List<HashTag>() };
            AddHashTagToAnswer("spicy", ans1, context);
            AddHashTagToAnswer("sweet", ans2, context);
            AddHashTagToAnswer("sour", ans2, context);
            AddHashTagToAnswer("sweet", ans3, context);
            AddHashTagToAnswer("sour", ans4, context);
            question.Answers.Add(ans1);
            question.Answers.Add(ans2);
            questions.Add(question);
            #endregion
            #region Which movie is your favourite one?
            question = new Question() { Value = "Which movie is your favourite one?", Answers = new List<Answer>() };
            ans1 = new Answer() { Value = "Titanic", HashTags = new List<HashTag>() };
            ans2 = new Answer() { Value = "Magnum Force", HashTags = new List<HashTag>() };
            ans3 = new Answer() { Value = "The goodfather", HashTags = new List<HashTag>() };
            ans4 = new Answer() { Value = "Finding Nemo", HashTags = new List<HashTag>() };
            AddHashTagToAnswer("romantic", ans1, context);
            AddHashTagToAnswer("sophisticated", ans1, context);
            AddHashTagToAnswer("american", ans2, context);
            AddHashTagToAnswer("meat", ans2, context);
            AddHashTagToAnswer("steak", ans2, context);
            AddHashTagToAnswer("italian", ans3, context);
            AddHashTagToAnswer("pasta", ans3, context);
            AddHashTagToAnswer("fish", ans4, context);
            AddHashTagToAnswer("water", ans4, context);
            question.Answers.Add(ans1);
            question.Answers.Add(ans2);
            question.Answers.Add(ans3);
            question.Answers.Add(ans4);
            questions.Add(question);
            #endregion
            #region Are you crazy?
            question = new Question() { Value = "Are you crazy?", Answers = new List<Answer>() };
            ans1 = new Answer() { Value = "Hell yeah!", HashTags = new List<HashTag>() };
            ans2 = new Answer() { Value = "Sometimes", HashTags = new List<HashTag>() };
            ans3 = new Answer() { Value = "Rahter not", HashTags = new List<HashTag>() };
            AddHashTagToAnswer("spicy", ans1, context);
            AddHashTagToAnswer("alcohol", ans1, context);
            AddHashTagToAnswer("exotic", ans1, context);
            AddHashTagToAnswer("casual", ans2, context);
            AddHashTagToAnswer("normal", ans2, context);
            AddHashTagToAnswer("american", ans2, context);
            AddHashTagToAnswer("italian", ans2, context);
            AddHashTagToAnswer("polish", ans3, context);
            AddHashTagToAnswer("german", ans3, context);
            question.Answers.Add(ans1);
            question.Answers.Add(ans2);
            question.Answers.Add(ans3);
            questions.Add(question);
            #endregion
            #region Do you mind waiting a long?
            question = new Question() { Value = "Do you mind waiting a long?", Answers = new List<Answer>() };
            ans1 = new Answer() { Value = "Yes", HashTags = new List<HashTag>() };
            ans2 = new Answer() { Value = "No", HashTags = new List<HashTag>() };
            AddHashTagToAnswer("fast", ans1, context);
            AddHashTagToAnswer("time_consuming", ans2, context);
            question.Answers.Add(ans1);
            question.Answers.Add(ans2);
            questions.Add(question);
            #endregion

            foreach (var q in questions)
            {
                context.Questions.Add(q);
            }
            context.SaveChanges();
            #endregion

            #region tables
            context.Tables.Add(new Table() {SeatsNumber = 4, TableNumber = 1});
            context.Tables.Add(new Table() {SeatsNumber = 3, TableNumber = 2});
            context.Tables.Add(new Table() {SeatsNumber = 3, TableNumber = 3});
            context.Tables.Add(new Table() {SeatsNumber = 2, TableNumber = 4});
            context.Tables.Add(new Table() {SeatsNumber = 2, TableNumber = 5});
            context.Tables.Add(new Table() {SeatsNumber = 3, TableNumber = 6});
            #endregion

            base.Seed(context);
        }

        protected virtual bool AddHashTagToMeal(string hashTag, Meal meal, ByfDbContext context)
        {
            // If meal already contains such hashtag
            if (meal.HashTags.Any(h => h.Name == hashTag.ToLower()))
            {
                return true;
            }
            // Check the existence of a hashtag 
            var tmpHashTag = context.HashTags.FirstOrDefault(h => h.Name == hashTag.ToLower());
            if (tmpHashTag == null)
            {
                meal.HashTags.Add(context.HashTags.Add(new HashTag { Name = hashTag.ToLower() }));
                context.SaveChanges();
                return true;
            }
            else
            {
                meal.HashTags.Add(tmpHashTag);
                return true;
            }
        }

        protected virtual bool AddIngredientToMeal(string ingredient, Meal meal, ByfDbContext context)
        {
            // If meal already contains such ingredient
            if (meal.Ingredients.Where(h => h.Name == ingredient.ToLower()).SingleOrDefault() != null)
            {
                return true;
            }
            // Check the existence of a hashtag 
            var tmpIngredient = context.Ingredients.Where(h => h.Name == ingredient.ToLower()).SingleOrDefault();
            if (tmpIngredient == null)
            {
                // No such ingredient, add exception
                return false;
            }
            else
            {
                meal.Ingredients.Add(tmpIngredient);
                context.SaveChanges();
                return true;
            }
        }

        protected virtual bool AddHashTagToIngredient(string hashTag, Ingredient ingredient, ByfDbContext context)
        {
            // If meal already contains such hashtag
            if (ingredient.HashTags.Where(h => h.Name == hashTag.ToLower()).SingleOrDefault() != null)
            {
                return true;
            }
            // Check the existence of a hashtag 
            var tmpHashTag = context.HashTags.Where(h => h.Name == hashTag.ToLower()).SingleOrDefault();
            if (tmpHashTag == null)
            {
                ingredient.HashTags.Add(context.HashTags.Add(new HashTag() { Name = hashTag.ToLower() }));
                context.SaveChanges();
                return true;
            }
            else
            {
                ingredient.HashTags.Add(tmpHashTag);
                return true;
            }
        }

        protected virtual bool AddHashTagToDrink(string hashTag, Drink drink, ByfDbContext context)
        {
            // If meal already contains such hashtag
            if (drink.HashTags.Where(h => h.Name == hashTag.ToLower()).SingleOrDefault() != null)
            {
                return true;
            }
            // Check the existence of a hashtag 
            var tmpHashTag = context.HashTags.Where(h => h.Name == hashTag.ToLower()).SingleOrDefault();
            if (tmpHashTag == null)
            {
                drink.HashTags.Add(context.HashTags.Add(new HashTag() { Name = hashTag.ToLower() }));
                context.SaveChanges();
                return true;
            }
            else
            {
                drink.HashTags.Add(tmpHashTag);
                return true;
            }
        }

        protected virtual bool AddHashTagToAnswer(string hashTag, Answer answer, ByfDbContext context)
        {
            // If meal already contains such hashtag
            if (answer.HashTags.Where(h => h.Name == hashTag.ToLower()).SingleOrDefault() != null)
            {
                return true;
            }
            // Check the existence of a hashtag 
            var tmpHashTag = context.HashTags.Where(h => h.Name == hashTag.ToLower()).SingleOrDefault();
            if (tmpHashTag == null)
            {
                answer.HashTags.Add(context.HashTags.Add(new HashTag() { Name = hashTag.ToLower() }));
                context.SaveChanges();
                return true;
            }
            else
            {
                answer.HashTags.Add(tmpHashTag);
                return true;
            }
        }
    }
}