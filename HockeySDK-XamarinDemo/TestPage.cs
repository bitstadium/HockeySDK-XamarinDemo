using System;

using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HockeySDKXamarinDemo
{
	public class TestPage : ContentPage
	{
		private StackLayout ContentLayout;

		public TestPage()
		{
			var ThrowBasicExceptionButton = new Button {
				Text = "Throw Basic Exception"
			};
			ThrowBasicExceptionButton.Clicked += ThrowBasicException;

			var ThrowLambdaExceptionButton = new Button {
				Text = "Throw Lambda Exception"
			};
			ThrowLambdaExceptionButton.Clicked += (sender, e) => {
				throw new TestHockeyAppException("This is an exception thrown from inside a lambda expression.");
			};

			var ThrowAwaitedBasicExceptionButton = new Button {
				Text = "Throw Awaited Basic Exception"
			};
			ThrowAwaitedBasicExceptionButton.Clicked += ThrowAwaitedBasicException;

			var ThrowAwaitedLambdaExceptionButton = new Button {
				Text = "Throw Awaited Lambda Exception"
			};
			ThrowAwaitedLambdaExceptionButton.Clicked += async (sender, e) => await Task.Run(() => {
				throw new TestHockeyAppException("This is an exception thrown from an awaited lambda expression.");
			});

			var ThrowExceptionWithExtraDataButton = new Button {
				Text = "Throw Exception with Extra Data"
			};
			ThrowExceptionWithExtraDataButton.Clicked += ThrowExceptionWithExtraData;

			var ThrowAggregateExceptionButton = new Button {
				Text = "Throw Aggregate Exception"
			};
			ThrowAggregateExceptionButton.Clicked += ThrowAggregateException;

			var ThrowParsingExceptionButton = new Button {
				Text = "Throw Parsing Exception"
			};
			ThrowParsingExceptionButton.Clicked += ThrowParsingException;

			var ThrowIndexOutOfBoundsExceptionButton = new Button {
				Text = "Throw Index Out of Bounds Exception"
			};
			ThrowIndexOutOfBoundsExceptionButton.Clicked += ThrowIndexOutOfBoundsException;

			var ThrowFileIOExceptionButton = new Button {
				Text = "Throw File IO Exception"
			};
			ThrowFileIOExceptionButton.Clicked += ThrowFileIOException;

			ContentLayout = new StackLayout { 
				Children = {
					ThrowBasicExceptionButton,
					ThrowLambdaExceptionButton,
					ThrowAwaitedBasicExceptionButton,
					ThrowAwaitedLambdaExceptionButton,
					ThrowExceptionWithExtraDataButton,
					ThrowAggregateExceptionButton,
					ThrowParsingExceptionButton,
					ThrowIndexOutOfBoundsExceptionButton,
					ThrowFileIOExceptionButton
				}
			};

			Content = ContentLayout;
		}

		private void ThrowBasicException(object sender, EventArgs e)
		{
			throw new TestHockeyAppException("This is a basic exception.");
		}

		private async void ThrowAwaitedBasicException(object sender, EventArgs e) {
			await Task.Run(() => { throw new TestHockeyAppException("This is an exception thrown from an awaited method."); });
		}

		private void ThrowExceptionWithExtraData(object sender, EventArgs e)
		{
			var exception = new TestHockeyAppException("This is an exception with extra data.");
			exception.Data.Add("Test1", "Data1");
			exception.Data.Add("Test2", "Data2");

			throw exception;
		}

		private void ThrowAggregateException(object sender, EventArgs e)
		{
			var exceptionList = new List<TestHockeyAppException>();

			try
			{
				throw new TestHockeyAppException("This is the first basic exception.");
			}
			catch(TestHockeyAppException ex)
			{
				exceptionList.Add(ex);
			}

			try
			{
				throw new TestHockeyAppException ("This is the second basic exception.");
			}
			catch(TestHockeyAppException ex)
			{
				exceptionList.Add(ex);
			}

			throw new AggregateException( "This is an aggregate exception with 2 inner exceptions.", exceptionList);
		}

		private void ThrowParsingException(object sender, EventArgs e)
		{
			int.Parse("This should throw a parsing exception.");
		}

		private void ThrowIndexOutOfBoundsException(object sender, EventArgs e)
		{
			var value = (new int[0]) [0];
		}

		private void ThrowFileIOException(object sender, EventArgs e)
		{
			DependencyService.Get<ISaveAndLoad>().LoadText("This file should not exist.");
		}

		public void AddChild(View child)
		{
			ContentLayout.Children.Add(child);
		}

		public void AddChildren(params View[] children)
		{
			foreach(var child in children)
				ContentLayout.Children.Add(child);
		}

		public void AddChildren(IEnumerable<View> children)
		{
			foreach(var child in children)
				ContentLayout.Children.Add(child);
		}
	}

	public class TestHockeyAppException : System.Exception
	{
		public TestHockeyAppException(string msg) : base(msg)
		{
		}
	}
}


