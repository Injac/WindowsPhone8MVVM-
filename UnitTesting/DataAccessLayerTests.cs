using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVVMWindowsPhone.Core.Portable.DAL;
using UnitTesting.Fakes;
using MVVMWindowsPhone.Core.Portable.Model;
using Microsoft.Phone.Testing;

namespace UnitTesting
{
    /// <summary>
    /// Testexample without 
    /// using Moq, creating 
    /// fakes manually.
    /// </summary>
    [TestClass]
    public class DataAccessLayerTests
    {

        /// <summary>
        /// The users
        /// </summary>
        List<User> users;

        /// <summary>
        /// The test repo
        /// </summary>
        IRepository<User,FakeDBContext> testRepo;



        /// <summary>
        /// Initializes the test.
        /// </summary>
        [TestInitialize]
        public void InitializeTest()
        {
            //Prepare the fake data
            users = new List<User>();

            users.Add(new User(){ Image="http://userimages.com/image1.png", Url="http://user1.com",UserName="TestUser1"});
            users.Add(new User() { Image = "http://userimages.com/image2.png", Url = "http://user2.com", UserName = "TestUser1" });
            users.Add(new User() { Image = "http://userimages.com/image3.png", Url = "http://user3.com", UserName = "TestUser2" });
            users.Add(new User() { Image = "http://userimages.com/image4.png", Url = "http://user4.com", UserName = "TestUser3" });
            users.Add(new User() { Image = "http://userimages.com/image5.png", Url = "http://user5.com", UserName = "TestUser4" });
            users.Add(new User() { Image = "http://userimages.com/image6.png", Url = "http://user6.com", UserName = "TestUser5" });
            users.Add(new User() { Image = "http://userimages.com/image7.png", Url = "http://user7.com", UserName = "TestUser6" });
            users.Add(new User() { Image = "http://userimages.com/image8.png", Url = "http://user8.com", UserName = "TestUser7" });
            users.Add(new User() { Image = "http://userimages.com/image9.png", Url = "http://user9.com", UserName = "TestUser8" });
            users.Add(new User() { Image = "http://userimages.com/image10.png", Url = "http://user10.com", UserName = "TestUser9" });
            users.Add(new User() { Image = "http://userimages.com/image11.png", Url = "http://user11.com", UserName = "TestUser10" });
            users.Add(new User() { Image = "http://userimages.com/image12.png", Url = "http://user12.com", UserName = "TestUser11" });
            users.Add(new User() { Image = "http://userimages.com/image13.png", Url = "http://user13.com", UserName = "TestUser12" });
            users.Add(new User() { Image = "http://userimages.com/image14.png", Url = "http://user14.com", UserName = "TestUser13" });
            users.Add(new User() { Image = "http://userimages.com/image15.png", Url = "http://user15.com", UserName = "TestUser14" });
            users.Add(new User() { Image = "http://userimages.com/image16.png", Url = "http://user16.com", UserName = "TestUser15" });
            users.Add(new User() { Image = "http://userimages.com/image17.png", Url = "http://user17.com", UserName = "TestUser16" });

            this.testRepo = new FakeUserRepository();
            
            var fakeUnitOfWork = new FakeUnitOfWork();
            
            fakeUnitOfWork.SetContext(new FakeDBContext(users));

            this.testRepo.Driver = fakeUnitOfWork;
        }



        /// <summary>
        /// Gets all users and count test.
        /// </summary>
        [TestMethod]
        public void GetAllUsersAndCountTest()
        {
                   
            var count = this.testRepo.GetAllEntries().Count();
            Assert.Equals(count,17);

        }

        /// <summary>
        /// Adds the user and count test.
        /// </summary>
        [TestMethod]
        public void AddUserAndCountTest()
        {
            this.testRepo.AddEntry(new User(){ Image="Image",Url="some url",UserName="Some UserName"});

            var count = testRepo.GetAllEntries().Count();

            Assert.Equals(count,18);
        }

        /// <summary>
        /// Users the filter test.
        /// </summary>
        [TestMethod]
        public void UserFilterTest()
        {

            var filteredUsers = testRepo.GetFilteredEntries(user=>user.UserName.Equals("TestUser1") && user.UserName.Equals("TestUser2")).ToList<User>();
                                  
            Assert.Equals(filteredUsers.Count(),2);
                       
        }

        /// <summary>
        /// Users the delete test.
        /// </summary>
        [TestMethod]
        [Tag("DeleteOnly")]
        public void UserDeleteTest()
        {
            var userToRemove = testRepo.GetFilteredEntries(user => user.UserName.Contains("TestUser1")).First();

            var deletedUser = testRepo.DeleteEntry(userToRemove);

            Assert.Equals(testRepo.Driver.Context.FakeTable.Count(), 16);
        }

        /// <summary>
        /// Updates the user test.
        /// </summary>
        [TestMethod]
        public void UpdateUserTest()
        {
            var userToUpdate  = testRepo.GetFilteredEntries(user => user.UserName.Contains("TestUser1")).First();

            var updatedEntry = new User(){UserName=userToUpdate.UserName, Image="changed",Url = userToUpdate.Url};

            testRepo.Driver.Context.UpdateEntry(userToUpdate,updatedEntry);

            var updatedUser  = testRepo.GetFilteredEntries(user => user.UserName.Contains("TestUser1")).First();

            Assert.Equals(updatedUser.Image, "changed");
            
        }
              
    }
}
