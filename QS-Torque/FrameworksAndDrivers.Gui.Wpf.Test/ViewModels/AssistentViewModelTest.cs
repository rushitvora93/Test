using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;

namespace FrameworksAndDrivers.Gui.Wpf.Test.ViewModels
{
    class AssistentViewModelTest
    {
        private AssistentViewModel _viewModel;

        [SetUp]
        public void AssistentViewModelTestSetup()
        {
            var assistentItem0 = new AssistentItemModel<string>(AssistentItemType.Text,
                                                                "Description0",
                                                                "AttributeName0",
                                                                "", 
                                                                (o, i) => { });
            var assistentItem1 = new AssistentItemModel<string>(AssistentItemType.Text,
                                                                "Description1",
                                                                "AttributeName1",
                                                                "",
                                                                (o, i) => { });
            var assistentItem2 = new ListAssistentItemModel<string>(Dispatcher.CurrentDispatcher,
                                                                    new List<string>() { "String 1", "String 2", "String 3" },
                                                                    "Description2",
                                                                    "AttributeName2",
                                                                    "", 
                                                                    (o, i) => { },
                                                                    "",
                                                                    x => x,
                                                                    () => { });

            var plan0 = new AssistentPlan<string>(assistentItem0);
            var plan1 = new AssistentPlan<string>(assistentItem1);
            var plan2 = new AssistentPlan<string>(assistentItem2);

            var plan = new ParentAssistentPlan(new List<ParentAssistentPlan>() { plan0, plan1, plan2 });

            _viewModel = new AssistentViewModel(plan);
        }
        
        [Test]
        public void AssistentPlanConstructorTest()
        {
            var assistentItem0 = new AssistentItemModel<string>(AssistentItemType.Text,
                                                                "Description0",
                                                                "AttributeName0",
                                                                "", 
                                                                (o, i) => { });
            var assistentItem1 = new AssistentItemModel<string>(AssistentItemType.Text,
                                                                "Description1",
                                                                "AttributeName1",
                                                                "", 
                                                                (o, i) => { });
            var assistentItem2 = new AssistentItemModel<string>(AssistentItemType.Text,
                                                                "Description2",
                                                                "AttributeName2",
                                                                "",
                                                                (o, i) => { });

            var plan0 = new AssistentPlan<string>(assistentItem0);
            var plan1 = new AssistentPlan<string>(assistentItem1);
            var plan2 = new AssistentPlan<string>(assistentItem2);

            var plan = new ParentAssistentPlan(new List<ParentAssistentPlan>() { plan0, plan1, plan2 });

            var viewModel = new AssistentViewModel(plan);

            Assert.AreEqual(viewModel.Plan, plan);
            Assert.AreEqual(viewModel.AssistentItemCollectionView.Count, 3);
            Assert.AreEqual(viewModel.AssistentItemCollectionView.GetItemAt(0), assistentItem0);
            Assert.AreEqual(viewModel.AssistentItemCollectionView.GetItemAt(1), assistentItem1);
            Assert.AreEqual(viewModel.AssistentItemCollectionView.GetItemAt(2), assistentItem2);
            Assert.AreEqual(viewModel.SelectedAssistentItem, assistentItem0);
            Assert.AreEqual(viewModel.SelectedAssistentItem.AlreadySelected, true);
        }

        [Test]
        public void SelectedItemWithTest()
        {
            _viewModel.SelectedAssistentItem = (AssistentItemModel)_viewModel.AssistentItemCollectionView.GetItemAt(0);
            _viewModel.SelectedAssistentItem = (AssistentItemModel)_viewModel.AssistentItemCollectionView.GetItemAt(1);

            Assert.AreEqual(_viewModel.PreviousSelectedItem, _viewModel.AssistentItemCollectionView.GetItemAt(0));
            Assert.AreEqual(_viewModel.SelectedAssistentItem, _viewModel.AssistentItemCollectionView.GetItemAt(1));
        }

        [TestCase(0)]
        [TestCase(1)]
        public void IsNotLastAssistentItemTest(int index)
        {
            _viewModel.SelectedAssistentItem = (AssistentItemModel)_viewModel.AssistentItemCollectionView.GetItemAt(index);

            Assert.AreEqual(_viewModel.IsLastAssistentItem, false);
        }

        [Test]
        public void IsLastAssistentItemTest()
        {
            _viewModel.SelectedAssistentItem = (AssistentItemModel)_viewModel.AssistentItemCollectionView.GetItemAt(2);

            Assert.AreEqual(_viewModel.IsLastAssistentItem, true);
        }

        [Test]
        public void ForwardCanExecuteFalseTest()
        {
            _viewModel.SelectedAssistentItem.IsSet = false;

            Assert.AreEqual(_viewModel.NextCommand.CanExecute(null), false);
        }

        [Test]
        public void ForwardCanExecuteTrueTest()
        {
            _viewModel.SelectedAssistentItem.IsSet = true;

            Assert.AreEqual(_viewModel.NextCommand.CanExecute(null), true);
        }

        [Test]
        public void ForewardErrorTest()
        {
            bool messageBoxInvoked = false;
            string errorText = "";

            _viewModel.SelectedAssistentItem.ErrorCheck = (x) => (x as AssistentItemModel<string>).EnteredValue == "FatalError";
            _viewModel.SelectedAssistentItem.ErrorText = "Text for Error";
            _viewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxInvoked = true;
                errorText = e.Text;
            };

            (_viewModel.SelectedAssistentItem as AssistentItemModel<string>).EnteredValue = "FatalError";
            _viewModel.NextCommand.Execute(null);

            Assert.AreEqual(messageBoxInvoked, true);
            Assert.AreEqual(errorText, "Text for Error");
        }

        [Test]
        public void ForewardNoErrorTest()
        {
            bool messageBoxInvoked = false;

            _viewModel.SelectedAssistentItem.ErrorCheck = (x) => (x as AssistentItemModel<string>).EnteredValue == "FatalError";
            _viewModel.MessageBoxRequest += (s, e) => messageBoxInvoked = true;

            (_viewModel.SelectedAssistentItem as AssistentItemModel<string>).EnteredValue = "NoError";
            _viewModel.NextCommand.Execute(null);

            Assert.AreEqual(messageBoxInvoked, false);
        }

        [Test]
        public void ForewardWarningTest()
        {
            bool messageBoxInvoked = false;
            string warningText = "";

            _viewModel.SelectedAssistentItem.WarningCheck = (x) => (x as AssistentItemModel<string>).EnteredValue == "WarningMessage";
            _viewModel.SelectedAssistentItem.WarningText = x => "Text for Warning";
            _viewModel.MessageBoxRequest += (s, e) =>
            {
                messageBoxInvoked = true;
                warningText = e.Text;
            };

            (_viewModel.SelectedAssistentItem as AssistentItemModel<string>).EnteredValue = "WarningMessage";
            _viewModel.NextCommand.Execute(null);

            Assert.AreEqual(messageBoxInvoked, true);
            Assert.AreEqual(warningText, "Text for Warning");
        }

        [Test]
        public void ForewardNoWarningTest()
        {
            bool messageBoxInvoked = false;

            _viewModel.SelectedAssistentItem.WarningCheck = (x) => (x as AssistentItemModel<string>).EnteredValue == "WarningMessage";
            _viewModel.MessageBoxRequest += (s, e) => messageBoxInvoked = true;

            (_viewModel.SelectedAssistentItem as AssistentItemModel<string>).EnteredValue = "NoWarning";
            _viewModel.NextCommand.Execute(null);

            Assert.AreEqual(messageBoxInvoked, false);
        }

        [Test]
        public void ForewardIsLastAssistentItemTest()
        {
            bool endOfAssistentInvoked = false;

            _viewModel.EndOfAssistent += (s, e) => endOfAssistentInvoked = true;
            _viewModel.SelectedAssistentItem = (AssistentItemModel)_viewModel.AssistentItemCollectionView.GetItemAt(2);

            _viewModel.NextCommand.Execute(null);

            Assert.IsTrue(endOfAssistentInvoked);
        }

        [Test]
        public void ForewardIsNotLastAssistentItemTest()
        {
            bool endOfAssistentInvoked = false;

            _viewModel.EndOfAssistent += (s, e) => endOfAssistentInvoked = true;
            _viewModel.SelectedAssistentItem = (AssistentItemModel)_viewModel.AssistentItemCollectionView.GetItemAt(1);

            _viewModel.NextCommand.Execute(null);

            Assert.AreEqual(endOfAssistentInvoked, false);
        }

        [Test]
        public void ForewardExecuteTest()
        {
            _viewModel.NextCommand.Execute(null);

            Assert.AreEqual(_viewModel.SelectedAssistentItem.AlreadySelected, true);
            Assert.AreEqual(_viewModel.SelectedAssistentItem, _viewModel.AssistentItemCollectionView.GetItemAt(1));
        }

        [Test]
        public void BackwardCanExecuteFalseTest()
        {
            Assert.AreEqual(_viewModel.PreviousCommand.CanExecute(null), false);
        }

        [Test]
        public void BackwardCanExecuteTrueTest()
        {
            _viewModel.SelectedAssistentItem = (AssistentItemModel)_viewModel.AssistentItemCollectionView.GetItemAt(1);

            Assert.AreEqual(_viewModel.PreviousCommand.CanExecute(null), true);
        }

        [Test]
        public void SelectionChangedCanExecuteTest()
        {
            Assert.AreEqual(_viewModel.SelectedAssistentItemChangedCommand.CanExecute(null), true);
        }

        [Test]
        public void BackwardExecuteTest()
        {
            _viewModel.SelectedAssistentItem = (AssistentItemModel)_viewModel.AssistentItemCollectionView.GetItemAt(1);

            _viewModel.PreviousCommand.Execute(null);

            Assert.AreEqual(_viewModel.SelectedAssistentItem, _viewModel.AssistentItemCollectionView.GetItemAt(0));
        }

        [Test]
        public void ResetSelectedItemTest()
        {
            var routedEvent = System.Windows.EventManager.RegisterRoutedEvent("Routed",
                System.Windows.RoutingStrategy.Bubble, typeof(System.Windows.RoutedEventHandler), typeof(AssistentViewModelTest));

            _viewModel.SelectedAssistentItemChangedCommand.Execute(new SelectionChangedEventArgs(routedEvent,
                new List<AssistentItemModel>() { (AssistentItemModel)_viewModel.AssistentItemCollectionView.GetItemAt(0) },
                new List<AssistentItemModel>() { (AssistentItemModel)_viewModel.AssistentItemCollectionView.GetItemAt(2) }));

            Assert.AreEqual(_viewModel.SelectedAssistentItem, _viewModel.AssistentItemCollectionView.GetItemAt(0));
        }

        [Test]
        public void ItemsEnterdValueAfterAssistententTest()
        {
            bool endOfAssistentInvoked = false;

            _viewModel.EndOfAssistent += (s, e) =>
            {
                endOfAssistentInvoked = true;

                var list = new List<AssistentPlan>();
                _viewModel.Plan.AddNext(list);

                Assert.AreEqual((_viewModel.AssistentItemCollectionView.GetItemAt(0) as AssistentItemModel<string>).EnteredValue, "Entered Value 1");
                Assert.AreEqual((_viewModel.AssistentItemCollectionView.GetItemAt(1) as AssistentItemModel<string>).EnteredValue, "Entered Value 2");
                Assert.AreEqual((_viewModel.AssistentItemCollectionView.GetItemAt(2) as ListAssistentItemModel<string>).EnteredValue, "String 2");
            };

            (_viewModel.SelectedAssistentItem as AssistentItemModel<string>).EnteredValue = "Entered Value 1";
            _viewModel.NextCommand.Execute(null);
            (_viewModel.SelectedAssistentItem as AssistentItemModel<string>).EnteredValue = "Entered Value 2";
            _viewModel.NextCommand.Execute(null);
            (_viewModel.SelectedAssistentItem as ListAssistentItemModel<string>).EnteredDisplayMemberModel = (_viewModel.SelectedAssistentItem as ListAssistentItemModel<string>).ItemsCollectionView.GetItemAt(1) as DisplayMemberModel<string>;
            _viewModel.NextCommand.Execute(null);

            Assert.AreEqual(endOfAssistentInvoked, true);
        }

        [Test]
        public void FillResultObjectTest()
        {
            (_viewModel.AssistentItemCollectionView.GetItemAt(0) as AssistentItemModel).SetAttributeValueToResultObject = 
                (o, i) => (o as DummyClass).Item0 = (i as AssistentItemModel<string>).EnteredValue;
            (_viewModel.AssistentItemCollectionView.GetItemAt(1) as AssistentItemModel).SetAttributeValueToResultObject =
                (o, i) => (o as DummyClass).Item1 = (i as AssistentItemModel<string>).EnteredValue;
            (_viewModel.AssistentItemCollectionView.GetItemAt(2) as AssistentItemModel).SetAttributeValueToResultObject =
                (o, i) => (o as DummyClass).Item2 = (i as AssistentItemModel<string>).EnteredValue;

            foreach (AssistentItemModel<string> i in _viewModel.AssistentItemCollectionView)
            {
                i.EnteredValue = i.Description;
            }

            var result = (DummyClass)_viewModel.FillResultObject(new DummyClass());

            Assert.AreEqual(result.Item0, (_viewModel.AssistentItemCollectionView.GetItemAt(0) as AssistentItemModel).Description);
            Assert.AreEqual(result.Item1, (_viewModel.AssistentItemCollectionView.GetItemAt(1) as AssistentItemModel).Description);
            Assert.AreEqual(result.Item2, (_viewModel.AssistentItemCollectionView.GetItemAt(2) as AssistentItemModel).Description);
        }

        [Test]
        public void NextCommandSkipsChapterHeading()
        {
            var assistentItem0 = new AssistentItemModel<string>(AssistentItemType.Text,
                "Description0",
                "AttributeName0",
                "",
                (o, i) => { });
            var chapterHeadingItem = new ChapterHeadingAssistentItemModel("");
            var assistentItem2 = new AssistentItemModel<string>(AssistentItemType.Text,
                "Description2",
                "AttributeName2",
                "",
                (o, i) => { });

            var plan0 = new AssistentPlan<string>(assistentItem0);
            var plan1 = new AssistentPlan(chapterHeadingItem);
            var plan2 = new AssistentPlan<string>(assistentItem2);

            var plan = new ParentAssistentPlan(new List<ParentAssistentPlan>() { plan0, plan1, plan2 });

            var viewModel = new AssistentViewModel(plan);

            Assert.AreEqual(assistentItem0, viewModel.SelectedAssistentItem);

            viewModel.NextCommand.Execute(null);

            Assert.AreEqual(assistentItem2, viewModel.SelectedAssistentItem);
            Assert.IsTrue(chapterHeadingItem.AlreadySelected);
        }

        [Test]
        public void NextCommandSkipsTwoChapterHeadings()
        {
            var assistentItem0 = new AssistentItemModel<string>(AssistentItemType.Text,
                "Description0",
                "AttributeName0",
                "",
                (o, i) => { });
            var chapterHeadingItem = new ChapterHeadingAssistentItemModel("");
            var chapterHeadingItem2 = new ChapterHeadingAssistentItemModel("");
            var assistentItem3 = new AssistentItemModel<string>(AssistentItemType.Text,
                "Description2",
                "AttributeName2",
                "",
                (o, i) => { });

            var plan0 = new AssistentPlan<string>(assistentItem0);
            var plan1 = new AssistentPlan(chapterHeadingItem);
            var plan2 = new AssistentPlan(chapterHeadingItem2);
            var plan3 = new AssistentPlan<string>(assistentItem3);

            var plan = new ParentAssistentPlan(new List<ParentAssistentPlan>() { plan0, plan1, plan2, plan3 });

            var viewModel = new AssistentViewModel(plan);

            Assert.AreEqual(assistentItem0, viewModel.SelectedAssistentItem);

            viewModel.NextCommand.Execute(null);

            Assert.AreEqual(assistentItem3, viewModel.SelectedAssistentItem);
            Assert.IsTrue(chapterHeadingItem.AlreadySelected);
            Assert.IsTrue(chapterHeadingItem2.AlreadySelected);
        }

        [Test]
        public void PreviousCommandSkipsChapterHeading()
        {
            var assistentItem0 = new AssistentItemModel<string>(AssistentItemType.Text,
                "Description0",
                "AttributeName0",
                "",
                (o, i) => { });
            var chapterHeadingItem = new ChapterHeadingAssistentItemModel("");
            var assistentItem2 = new AssistentItemModel<string>(AssistentItemType.Text,
                "Description2",
                "AttributeName2",
                "",
                (o, i) => { });

            var plan0 = new AssistentPlan<string>(assistentItem0);
            var plan1 = new AssistentPlan(chapterHeadingItem);
            var plan2 = new AssistentPlan<string>(assistentItem2);

            var plan = new ParentAssistentPlan(new List<ParentAssistentPlan>() { plan0, plan1, plan2 });

            var viewModel = new AssistentViewModel(plan);
            viewModel.SelectedAssistentItem = assistentItem2;
            
            viewModel.PreviousCommand.Execute(null);

            Assert.AreEqual(assistentItem0, viewModel.SelectedAssistentItem);
        }

        [Test]
        public void PreviousCommandSkipsTwoChapterHeadings()
        {
            var assistentItem0 = new AssistentItemModel<string>(AssistentItemType.Text,
                "Description0",
                "AttributeName0",
                "",
                (o, i) => { });
            var chapterHeadingItem = new ChapterHeadingAssistentItemModel("");
            var chapterHeadingItem2 = new ChapterHeadingAssistentItemModel("");
            var assistentItem3 = new AssistentItemModel<string>(AssistentItemType.Text,
                "Description2",
                "AttributeName2",
                "",
                (o, i) => { });

            var plan0 = new AssistentPlan<string>(assistentItem0);
            var plan1 = new AssistentPlan(chapterHeadingItem);
            var plan2 = new AssistentPlan(chapterHeadingItem2);
            var plan3 = new AssistentPlan<string>(assistentItem3);

            var plan = new ParentAssistentPlan(new List<ParentAssistentPlan>() { plan0, plan1, plan2, plan3 });

            var viewModel = new AssistentViewModel(plan);
            viewModel.SelectedAssistentItem = assistentItem3;

            viewModel.PreviousCommand.Execute(null);

            Assert.AreEqual(assistentItem0, viewModel.SelectedAssistentItem);
        }


        private class DummyClass
        {
            public string Item0 { get; set; }
            public string Item1 { get; set; }
            public string Item2 { get; set; }
        }
    }
}