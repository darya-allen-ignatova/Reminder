using System;
using System.Collections.Generic;
using DI.Reminder.BL.CachedRepository;
using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Data.CategoryDataBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace DI.Reminder.BL.Tests.Categories
{
    [TestClass]
    public class CategoryStorageTesting
    {
        private Mock<ICacheRepository> _cacheRepository;
        private Mock<ICategoryRepository> _categoryRepository;
        private List<Category> _testingListOfCategories;
        private CategoryStorage.Categories _categories;

        #region TestInitialize
        [TestInitialize]
        public void TestInitialize()
        {
            
            _cacheRepository = new Mock<ICacheRepository>();
            _categoryRepository = new Mock<ICategoryRepository>();
            _categories = new CategoryStorage.Categories(_categoryRepository.Object, _cacheRepository.Object);
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
        #endregion


        #region GetCategories
        [TestMethod]
        public void GetCategories_InvalidInputData_Null()
        {
            //
            //Arrange
            //
            int? id = null;
            //
            //Act
            //
            var result = _categories.GetCategories(id);
            //
            //Assert
            //
            Assert.AreEqual(null, result);
            _categoryRepository.Verify(m => m.GetAllCategories(), Times.Never);

        }
        [TestMethod]
        public void GetCategories_ValidData_ListOfCategories()
        {
            //
            //Arrange
            //
            int? id = 1;
            _categoryRepository.Setup(m => m.GetCategories(It.IsAny<int?>())).Returns(_testingListOfCategories.Where(t => t.ParentID == 1).ToList);
            //
            //Act
            //
            var result = _categories.GetCategories(id);
            //
            //Assert
            //
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(_testingListOfCategories[1].Name, result.FirstOrDefault(m => m.ParentID == 1).Name);
            _categoryRepository.Verify(m => m.GetCategories(It.IsAny<int?>()), Times.Once);
        }
        #endregion


        #region GetCategory
        [TestMethod]
        public void GetCategory_InvalidInputData_Null()
        {
            //
            //Arrange
            //
            int? id = -1;
            //
            //Act
            //
            var result = _categories.GetCategory(id);
            //
            //Assert
            //
            Assert.AreEqual(null, result);
            _categoryRepository.Verify(m => m.GetAllCategories(), Times.Never);
        }
        [TestMethod]
        public void GetCategory_validInputDataNoCache_Category()
        {
            //
            //Arrange
            //
            int ID = 3;
            _categoryRepository.Setup(m => m.GetCategory(It.IsAny<int>())).Returns(_testingListOfCategories.Where(m => m.ID == ID).FirstOrDefault);
            _cacheRepository.Setup(m => m.GetValueOfCache<Category>(It.IsAny<int>())).Returns<Category>(null);
            //
            //Act
            //
            Category result = _categories.GetCategory(ID);
            //
            //Assert
            //
            Assert.AreEqual(_testingListOfCategories[2].Name, result.Name);
            _cacheRepository.Verify(c => c.GetValueOfCache<Category>(It.IsAny<int>()), Times.Once);
            _cacheRepository.Verify(c => c.AddCache<Category>(It.IsAny<Category>(), It.IsAny<int>()), Times.Once);
            _categoryRepository.Verify(m => m.GetCategory(It.IsAny<int>()), Times.Once);

        }
        [TestMethod]
        public void GetCategory_validInputDataWithCache_Category()
        {
            //
            //Arrange
            //
            int ID = 3;
            _categoryRepository.Setup(m => m.GetCategory(It.IsAny<int>())).Returns<Category>(null);
            _cacheRepository.Setup(m => m.GetValueOfCache<Category>(It.IsAny<int>())).Returns(_testingListOfCategories.Where(m => m.ID==ID).FirstOrDefault);
            //
            //Act
            //
            Category result = _categories.GetCategory(ID);
            //
            //Assert
            //
            Assert.AreEqual(_testingListOfCategories[2].Name, result.Name);
            _cacheRepository.Verify(c => c.GetValueOfCache<Category>(It.IsAny<int>()), Times.Once);
            _cacheRepository.Verify(c => c.AddCache<Category>(It.IsAny<Category>(), It.IsAny<int>()), Times.Never);
            _categoryRepository.Verify(m => m.GetCategory(It.IsAny<int>()), Times.Never);

        }

        #endregion


        #region GetCategoryParentID
        [TestMethod]
        public void GetCategoryParentID_validInputData_ParentID()
        {
            //
            //Arrange
            //
            string Name="Work";
            _categoryRepository.Setup(m => m.GetCategoryParentID(It.IsAny<string>())).Returns(_testingListOfCategories.FirstOrDefault( m => m.Name==Name).ParentID);
            //
            //Act
            //
            int? result = _categories.GetCategoryParentID(Name);
            //
            //Assert
            //
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
            _categoryRepository.Verify(m => m.GetCategoryParentID(It.IsAny<string>()), Times.Once);

        }
        [TestMethod]
        public void GetCategoryParentID_InvalidInputData_Null()
        {
            //
            //Arrange
            //
            string Name = null;
            //
            //Act
            //
            int? result = _categories.GetCategoryParentID(Name);
            //
            //Assert
            //
            Assert.IsNull(result);
            _categoryRepository.Verify(m => m.GetCategoryParentID(It.IsAny<string>()), Times.Never);

        }
        #endregion


        #region GetCategoryIDByName
        [TestMethod]
        public void GetCategoryIDByName_InvalidInputData_Null()
        {
            //
            //Arrange
            //
            string Name = null;
            //
            //Act
            //
            int? result = _categories.GetCategoryIDByName(Name);
            //
            //Assert
            //
            Assert.IsNull(result);
            _categoryRepository.Verify(m => m.GetCategoryParentID(It.IsAny<string>()), Times.Never);

        }
        [TestMethod]
        public void GetCategoryIDByName_validInputData_ID()
        {
            //
            //Arrange
            //
            string Name = "Other";
            _categoryRepository.Setup(m => m.GetCategoryParentID(It.IsAny<string>())).Returns(_testingListOfCategories.FirstOrDefault(m => m.Name == Name).ID);
            //
            //Act
            //
            int? result = _categories.GetCategoryParentID(Name);
            //
            //Assert
            //
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result);
            _categoryRepository.Verify(m => m.GetCategoryParentID(It.IsAny<string>()), Times.Once);

        }
        #endregion
    }
}
