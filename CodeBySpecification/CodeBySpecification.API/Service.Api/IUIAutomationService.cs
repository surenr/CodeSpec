namespace CodeBySpecification.API.Service.Api
{
	public interface IUiAutomationService
	{
		string GetElementText(string elementKey, string selectionType = null, string selection = null);

		void IsElementContentEqual(string elementKey, string expectedContent, string selectionMethod = null, string selection = null);

		void ClickOn(string elementKey, string selectionMethod = null, string selection = null);

		void DragAndDrop(string dragElementKey, string dropElementKey, string dragElementSelectionMethod = null, string dragElementSelection = null, string dropElementKeySelectionMethod = null, string dropElementKeySelection = null);

		void EnterTextTo(string elementKey, string value, string selectionMethod = null, string selection = null);

        void SelectValueOf(string elementKey, string value, string selectionMethod = null, string selection = null);

        string SutUrl { get; }

		object GetBrowser { get; set; }

		void GotoUrl(string p);

		void IsElementVisible(string elementKey, string selectionMethod = null, string selection = null);

		void IsElementNotVisible(string elementKey, string selectionMethod = null, string selection = null);

		void AcceptTheConfirmation();

		void InitilizeTests(string browserType, string objectRepoResource);

		string ReadUrl();

		void AreValuesEqual(string value1, string value2);

		void IsPageContainsTextPattern(string textPattern);

		void IsElementContainsTextPattern(string elementKey, string selectionMethod, string selection = null, string textPattern = null);

		void ClickOn(string elementKey, int timeout, string selectionMethod = null, string selection = null);

		void SwitchToWindow(int tab);

		void CloseWindow(int tab);

		void TableHasRowCountOf(string elementKey, int numberOfRows);

		void TableHasColumnCountOf(string elementKey, int columnCount);

		void ValueOfTableRowColEqualTo(string elementKey, int row, int col, string value);
        void switchToFrame(string selectionMethod, string selection);
        void switchToDefaultContent();
        void GetTheValuesFrom(string dataRepo);
    }
}
