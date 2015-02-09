using System.Collections.Generic;
using CodeBySpecification.API.Domain;

namespace CodeBySpecification.API.Service.Api
{
	public interface IUIAutomationService
	{
		string GetElementText(string elementKey);

		string GetElementText(string elementKey, string selectionType, string selection);

		void IsElementContentEqual(string elementKey, string expectedContent);

		void IsElementContentEqual(string elementKey, string selectionMethod, string selection, string expectedContent);

		void ClickOn(string elementKey);

		void ClickOn(string elementKey, string selectionMethod, string selection);

		void DragAndDrop(string dragElementKey, string dropElementKey);

		void DragAndDrop(string dragElementKey, string dragElementSelectionMethod, string dragElementSelection, string dropElementKey, string dropElementKeySelectionMethod, string dropElementKeySelection);

		void EnterTextTo(string elementKey, string value);

		void EnterTextTo(string elementKey, string value, string selectionMethod, string selection);

		string SutUrl { get; }
		IDictionary<string, UiElement> ObjectRepo { get; set; }

		object GetBrowser { get; set; }

		void GotoUrl(string p);

		void IsElementVisible(string elementKey);

		void IsElementVisible(string elementKey, string selectionMethod, string selection);

		void AcceptTheConfirmation();

		void InitilizeTests(string browserType, string objectRepoSourcePath);
	}
}
