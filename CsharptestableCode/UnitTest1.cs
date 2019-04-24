using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CsharptestableCode.Tests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// How to test the Using Operator function
        /// </summary>
        [TestMethod]
        public void UsingSaveSmokeTest()
        {
            var mockXMLOperator = new Mock<IXMLOperator>();
            //whenever go to XML_File_Save, it will return true;
            mockXMLOperator.Setup(m => m.XML_File_Save("Test text", "savetest", false)).Returns(true);

            UsingOperator objUsingOperator = new UsingOperator();

            Assert.AreEqual(objUsingOperator.UsingSave(mockXMLOperator.Object), true);
        }


        /// <summary>
        /// Test the save load function themself
        /// </summary>
        [TestMethod]
        public void SaveSmokeTest()
        {
            
            //var mockSaveFactory = new Mock<ISaveFactory>();

            ConcreteSettingFactory factory = new ConcreteSettingFactory();
            ISaveFactory roughData = factory.GetSavedClass("1");


            ////whenever go to XML_File_Save, it will return true;
            //mockSaveFactory.Setup(m => factory.GetSavedClass("RobotSetupSetting"));

            IXMLOperator objUsingOperator = new xmlOperater();
			
			//don't forget to creat the path at first, or this test will be failed
            Assert.IsTrue(objUsingOperator.XML_File_Save(roughData, "AutoSave", false));

            Assert.IsTrue(objUsingOperator.XML_File_Load(ref roughData, false));
        }

    }
}
