using Moq;
using DI.Reminder.BL.CachedRepository;
using DI.Reminder.Data.CategoryDataBase;
using DI.Reminder.Data.PromptDataBase;
using DI.Reminder.Data.Searching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DI.Reminder.Common.PromptModel;
using DI.Reminder.Common.CategoryModel;
using System.Linq;

namespace DI.Reminder.BL.Tests.Prompts
{
    [TestClass]
    public class PromptStorageTesting
    {
        private PromptStorage.Prompts _prompts;
        private Mock<ICacheRepository> _cacheRepository;
        private Mock<ISearch> _search;
        private Mock<IPromptRepository> _promptRepository;
        private Mock<ICategoryRepository> _categoryRepository;
        private List<Prompt> _testingListOfPrompts;
        private List<Category> _testingListOfCategories;
        [TestInitialize]
        public void TestInitialize()
        {
            _cacheRepository = new Mock<ICacheRepository>();
            _search = new Mock<ISearch>();
            _promptRepository = new Mock<IPromptRepository>();
            _categoryRepository = new Mock<ICategoryRepository>();

            _prompts = new PromptStorage.Prompts(_promptRepository.Object, _categoryRepository.Object, _search.Object, _cacheRepository.Object);

            _testingListOfPrompts = new List<Prompt>()
            {
                new Prompt()
                {
                    ID=1,
                    userID=2,
                    Name="Make site start page",
                    Category="Work",
                    CreatingDate=new System.DateTime(2018,06,15),
                    Description="Until it is June,15",
                    Actions=new List<Action>()
                    {
                        new Action()
                        {
                            ID=1,
                            Name="Make html"
                        },
                        new Action()
                        {
                            ID=2,
                            Name="Make css"
                        },
                        new Action()
                        {
                            ID=3,
                            Name="Make js"
                        }
                    },
                    Image="ImagePath1",
                    TimeOfPrompt=new System.TimeSpan(12,12,0)
                },
                new Prompt()
                {
                    ID=2,
                    userID=2,
                    Name="Buy clocks for father",
                    Category="Family",
                    CreatingDate=new System.DateTime(2018,01,06),
                    Description="Now",
                    Actions=new List<Action>()
                    {
                        new Action()
                        {
                            ID=1,
                            Name="Find shop"
                        },
                        new Action()
                        {
                            ID=2,
                            Name="Buy"
                        }
                    },
                    Image="ImagePath2",
                    TimeOfPrompt=new System.TimeSpan(16,0,0)
                },
                new Prompt()
                {
                    ID =3,
                    userID=2,
                    Name="Paint picture",
                    Category="Other",
                    CreatingDate=new System.DateTime(2018,10,06),
                    Description="For dining room",
                    Actions=new List<Action>()
                    {
                        new Action()
                        {
                            ID=1,
                            Name="Buy layout"
                        },
                        new Action()
                        {
                            ID=2,
                            Name="Paint something wonderful"
                        }
                    },
                    Image="ImagePath3",
                    TimeOfPrompt=new System.TimeSpan(15,30,0)

                },
            };



            _testingListOfCategories = new List<Category>()
            {
                new Category()
                {
                    ID=1,
                    Name="Important",
                    ParentID=null
                },
                new Category()
                {
                    ID =2,
                    Name="Work",
                    ParentID=1
                },
                new Category()
                {
                    ID=3,
                    Name="Family",
                    ParentID=1
                },
                new Category()
                {
                    ID=4,
                    Name="Other",
                    ParentID=null
                }
            };


        }
        //[TestMethod]
        //public void GetCategoryItemsByID_validIDAndUserID_ListOfCategoryPrompts()
        //{
        //    //
        //    //Arrange
        //    //
        //    int UserID = 2;
        //    int ID = 1;

        //    Category category = _categoryRepository.(m => m.GetCategory(ID)).Returns(_testingListOfCategories.Where(m => m.ID == ID).FirstOrDefault);
        //    _promptRepository.Setup(m => m.GetPromptsList(UserID, ID)).Returns(_testingListOfPrompts.Where(m => m.Category==category.Name && m.userID == UserID).ToList);


        //    //
        //    //Act
        //    //
        //    IList<Prompt> result = _prompts.GetCategoryItemsByID(UserID, ID);

        //    //
        //    //Assert
        //    //

        //}
        [TestMethod]
        public void GetPromptDetails_InvalidID_Null()
        {
            //
            //Arrange
            //
            int userID = 2;
            int ID = -1;
            //
            //Act
            //
            Prompt result = _prompts.GetPromptDetails(userID, ID);
            //
            //Assert
            //
            Assert.AreEqual(result, null);
        }

    }
}
