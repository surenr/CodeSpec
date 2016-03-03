var testToDoItems = [
  {
	  Id: "1",
	  addedDate: new Date("2016-02-05T11:20:00"),
	  scheduledDate: new Date(2016,2,25),
	  body: 'Talk to Andre and schedule a meeting for Monday.',
	  inLists: [0,1],
	  isCompleted: true
  },
  {
	  Id: "2",
	  addedDate: new Date("2016-02-05T11:30:00"),
	  scheduledDate: new Date(2016,2,26),
	  body: 'Buy medecin for Thinithi',
	  inLists: [0,1],
	  isCompleted: true
  },
  {
	  Id: "3",
	  addedDate: new Date("2016-02-06T10:30:00"),
	  scheduledDate: new Date(2016,2,26),
	  body: 'Do Exersises and Buy books',
	  inLists: [0,2],
	  isCompleted: false
  },
  {
	  Id: "4",
	  addedDate: new Date("2016-02-06T11:30:00"),
	  scheduledDate: new Date(2016,2,27),
	  body: 'Code Review all pending tasks',
	  inLists: [1,2],
	  isCompleted: false
  },
  {
	  Id: "5",
	  addedDate: new Date("2016-02-06T11:30:00"),
	  scheduledDate: new Date(2016,2,28),
	  body: 'Buy gift fro wife',
	  inLists: [2],
	  isCompleted: false
  }

];
describe("List Manager",function(){
	it("Test List Manager object to exists in window",function(){
		expect(typeof window.MeDo.ListManager).toBe('object');
	});
	
		
	it("We can get the full set of avilable lists",function(){
		var listObjects = window.MeDo.ListManager.GetList();
		expect(listObjects.length).toBe(3);
		expect(listObjects[0].Id).toBe("general");
	});
	
	it("We can get the index of the list by list ID",function(){
		expect(window.MeDo.ListManager.GetListById("general")).toBe(0)
		expect(window.MeDo.ListManager.GetListById("personal")).toBe(2);
	});
	
	it("Trying to get the index of a list item by list ID witout supplying a valid ID returns -1",
		function(){
			expect(window.MeDo.ListManager.GetListById()).toBe(-1);	
	});
	
	it("Trying to add empty object to list throws an exception",function(){
		expect(function(){window.MeDo.ListManager.AddList()}).toThrow(new window.MeDo.Error("ERROR_012"));
	});
	
	it("Trying to add list object with empty ID thows an exception",function(){
		expect(function(){window.MeDo.ListManager.AddList(
			{Id:"",Name:"Test Name",Color: "#e3e3e3"}
		)}).toThrow(new window.MeDo.Error("ERROR_012"));
	});
	it("Trying to add list object with empty Name thows an exception",function(){
		expect(function(){window.MeDo.ListManager.AddList(
			{Id:"valid_id",Name:"",Color: "#e3e3e3"}
		)}).toThrow(new window.MeDo.Error("ERROR_012"));
	});
	
	it("Trying to add list object with empty Color thows an exception",function(){
		expect(function(){window.MeDo.ListManager.AddList(
			{Id:"valid_id",Name:"name",Color: ""}
		)}).toThrow(new window.MeDo.Error("ERROR_012"));
	});
	
	it("We can add a valid list object",function(){
		expect(window.MeDo.ListManager.GetListById("valid_id")).toBe(-1);
		window.MeDo.ListManager.AddList({Id:"valid_id",Name:"name",Color: "#e3e3e3"});
		expect(window.MeDo.ListManager.GetListById("valid_id")).not.toBe(-1);
	});
	
	it("We can remove list by providing a valid id",function(){
		window.MeDo.ListManager.AddList({Id:"test_list",Name:"name",Color: "#e3e3e3"});
		expect(window.MeDo.ListManager.GetListById("test_list")).not.toBe(-1);
		window.MeDo.ListManager.RemoveList({Id:"test_list"});
		expect(window.MeDo.ListManager.GetListById("test_list")).toBe(-1);
	});
	
	it("Trying to remove list with invalid list object throws an exception",function(){
		expect(function(){window.MeDo.ListManager.RemoveList()}).toThrow(new window.MeDo.Error("ERROR_012"));
	});
	
	it("Trying to remove list with invalid list object ID throws an exception",function(){
		expect(function(){window.MeDo.ListManager.RemoveList({Name: "test"})}).toThrow(new window.MeDo.Error("ERROR_012"));
	});
	
	it("Trying to remove list with none existing id throws an exception",function(){
		expect(function(){window.MeDo.ListManager.RemoveList({Id:"not_in_list"})}).toThrow(new window.MeDo.Error("ERROR_011"));
	});
});

describe("Task manager",function(){
	
	it("Test TaskManager Object Exists in window",function(){
		expect(typeof window.MeDo.TaskManager).toBe('object');
	});
	
	it("We can get the full list of tasks",function(){
		var taskList = window.MeDo.TaskManager.GetTasks();
		expect(taskList.length).toBe(5);
	});
	
	it("We can get the next valid task id",function(){
		var nextTaskId = window.MeDo.TaskManager.GeNextTaskId();
		expect(nextTaskId).toBe("6");
	});
	
	it("Adding a task with the same ID will throw an Exception",function(){
		expect(function(){
			window.MeDo.TaskManager.addTask({Id: "1"});
		}).toThrow(new window.MeDo.Error("ERROR_005"));
		
	});
	
	it("We can add task objects to the task list",function(){
		var objectToAdd = {
		  Id: window.MeDo.TaskManager.GeNextTaskId(),
		  addedDate: new Date("2016-02-05T11:20:00"),
		  scheduledDate: new Date(2016,2,26),
		  body: 'Talk to Andre and schedule a meeting for Monday.',
		  inLists: [],
		  isCompleted: false
	  };
	  var previousLength = window.MeDo.TaskManager.GetTasks().length;
	  window.MeDo.TaskManager.addTask(objectToAdd);
	  var afterLength = window.MeDo.TaskManager.GetTasks().length;
	  expect(afterLength).toBe((previousLength+1));
	});
	
	it("Trying to update an none existing task object throws an exception",function(){
		var objectToUpdate = {
		  Id: window.MeDo.TaskManager.GeNextTaskId(),
		  addedDate: new Date("2016-02-05T11:20:00"),
		  scheduledDate: new Date(2016,2,26),
		  body: 'Talk to Andre and schedule a meeting for Monday.',
		  inLists: [],
		  isCompleted: false
	  };
	  expect(function(){window.MeDo.TaskManager.UpdateTask(objectToUpdate);}).toThrow(new window.MeDo.Error("ERROR_006"));
	});
	
	it("We can get task Object by Id",function(){
		var object = window.MeDo.TaskManager.GetTaskById("1");
		expect(object.isCompleted).toBe(true);
		expect(object.body).toBe("Talk to Andre and schedule a meeting for Monday.");
	});
	
	it("We can update an existing task object",function(){
		var objectToUpdate = {
		  Id: "1",
		  isCompleted: false
	  };
	  window.MeDo.TaskManager.UpdateTask(objectToUpdate);
	  var object = window.MeDo.TaskManager.GetTaskById("1");
	  expect(object.isCompleted).toBe(false);
	});
	
	it("Trying to remove task with invalid task id will result in error",function(){
		var objectToDelete = {
		  Id: "invalid task"
	  };
	  expect(function(){window.MeDo.TaskManager.RemoveTask(objectToDelete)}).toThrow(new window.MeDo.Error("ERROR_006"));
	});
	
	it("We can remove tasks by task Id",function(){
		var taskId = window.MeDo.TaskManager.GeNextTaskId();
		var objectToAdd = {
		  Id: taskId,
		  addedDate: new Date("2016-02-05T11:20:00"),
		  scheduledDate: new Date(2016,2,26),
		  body: 'Talk to Andre and schedule a meeting for Monday.',
		  inLists: [],
		  isCompleted: false
	  };
	  var previousLength = window.MeDo.TaskManager.GetTasks().length;
	  window.MeDo.TaskManager.addTask(objectToAdd);
	  var afterLength = window.MeDo.TaskManager.GetTasks().length;
	  expect(afterLength).toBe((previousLength+1));
	  
	  var objectToDelete = {
		  Id: taskId
	  };
	  window.MeDo.TaskManager.RemoveTask(objectToDelete);
	  var afterDeleteLength = window.MeDo.TaskManager.GetTasks().length;
	  expect(afterDeleteLength).toBe(previousLength);
	})
});

describe("MeDo Application",function(){
	//setup MeDo app test data
	var randomColors = ["#C63D0F","#3B3738","#7E8F7C","#005A31","#A8CD1B","#CBE32D","#F3FAB6","#558C89","#74AFAD","#D9853B","#DE1B1B","#7D1935","#4A96AD","#E44424","#FF9009","#DF3D82"]
	var myLists = [
		{Id:"general", Name: "General",Color: "#C63D0F"},
		{Id:"home", Name: "Home",Color: "#3B3738"},
		{Id:"personal", Name: "Personal",Color: "#7E8F7C"}
	];

	//MeDo Application Tests
	it("Test Jesmin works",function(){
		expect(true).toBe(true);
	}); 
	
	it("Test MeDo Object Exists in window",function(){
		expect(typeof window.MeDo).toBe('object');
	});
	
	it("Adding new task throws error if task body is not set",function(){
		var taskBody = "";
		var taskDateTime = new Date();
		expect(function(){window.MeDo.App.addNewTask(taskBody,taskDateTime)}).toThrow(new window.MeDo.Error("ERROR_002"));
		
	});
	
	it("Adding new task throws error if nond date object is given as a task date",function(){
		var taskBody = "Test Task";
		var taskDateTime = null;
		expect(function(){window.MeDo.App.addNewTask(taskBody,taskDateTime)}).toThrow(new window.MeDo.Error("ERROR_003"));
		
	});
	
	it("Adding new task throws error if date is befoe today",function(){
		var taskBody = "Test Task";
		var taskDateTime = new Date(2011,10,30);
		expect(function(){window.MeDo.App.addNewTask(taskBody,taskDateTime)}).toThrow(new window.MeDo.Error("ERROR_004"));
		
	});
	
	it("Adding new valid task add successfully to taskList",function(){
		var taskBody = "Test Task";
		var testToDoItems = window.MeDo.TaskManager.GetTasks();
		var previousLength = testToDoItems.length;
		var taskDateTime = (new Date())+1;
		window.MeDo.App.addNewTask(taskBody,taskDateTime)
		expect(testToDoItems.length).toBe((previousLength+1));
		expect(testToDoItems[previousLength].body).toBe(taskBody);
		expect(testToDoItems[previousLength].scheduledDate).toBe(taskDateTime);
	});
	
	//marking task as completed
	it("Marking task as completed set the isCompleted to true",function(){
		var taskId = "3"
		var before = window.MeDo.TaskManager.GetTaskById(taskId);
		expect(before.isCompleted).toBe(false);
		window.MeDo.App.CompleteTask(taskId)
		var after = window.MeDo.TaskManager.GetTaskById(taskId);
		expect(after.isCompleted).toBe(true);
		
	});
	
	it("Marking none existing task as completed throws an exception",function(){
		var taskId = "13";
		expect(function(){window.MeDo.App.CompleteTask(taskId)}).toThrow(new window.MeDo.Error("ERROR_006"));
	});
	
	// re-opening a completed task
	it("Re-opening none existing task throws an exception",function(){
		var taskId = "13";
		expect(function(){window.MeDo.App.ReOpenTask(taskId)}).toThrow(new window.MeDo.Error("ERROR_006"));
	});
	
	it("Re-opening a task set the isCompleted to false",function(){
		var taskId = "2"
		var before = window.MeDo.TaskManager.GetTaskById(taskId);
		expect(before.isCompleted).toBe(true);
		window.MeDo.App.ReOpenTask(taskId)
		var after = window.MeDo.TaskManager.GetTaskById(taskId);
		expect(after.isCompleted).toBe(false);
		
	});
	
	//editing tasks
	it("Empty task body throw exceptoin in task edit",function(){
		var taskBody = "";
		var scheduledDate = new Date(2016,05,24);
		var taskId = "1";
		expect(function(){window.MeDo.App.UpdateTask(taskId,taskBody,scheduledDate)}).toThrow(new window.MeDo.Error("ERROR_007"));
			
	});
	
	it("A date before today as the scheduledDate throw exceptoin in task edit",function(){
		var taskBody = "new task body";
		var scheduledDate = new Date(2011,05,24);
		var taskId = "1";
		expect(function(){window.MeDo.App.UpdateTask(taskId,taskBody,scheduledDate)}).toThrow(new window.MeDo.Error("ERROR_008"));
			
	});
	
	it("Can edit only the task body without updating the schduled date.",function(){
		var taskBody = "new task body";
		var scheduledDate = "";
		var taskId = "1";
		var before = window.MeDo.TaskManager.GetTaskById(taskId);
		expect(before.body).toBe("Talk to Andre and schedule a meeting for Monday.");
		window.MeDo.App.UpdateTask(taskId,taskBody,scheduledDate);
		var after = window.MeDo.TaskManager.GetTaskById(taskId);
		expect(before.scheduledDate).toBe(after.scheduledDate);
		expect(after.body).toBe(taskBody);
			
	});
	
	it("Can edit and update both task body and schduled date.",function(){
		var taskBody = "updated task body";
		var scheduledDate = (new Date())+2;
		var taskId = "1";
		var before = window.MeDo.TaskManager.GetTaskById(taskId);
		expect(before.body).toBe("new task body");
		expect(before.scheduledDate).not.toBe(scheduledDate);
		window.MeDo.App.UpdateTask(taskId,taskBody,scheduledDate);
		var after = window.MeDo.TaskManager.GetTaskById(taskId);
		expect(after.scheduledDate).toBe(scheduledDate);
		expect(after.body).toBe(taskBody);
			
	});
	
	it("Using an invalid taskId throws an exception",function(){
		var taskBody = "updated task body";
		var scheduledDate = (new Date())+2;
		var taskId = "invalid id";
		expect(function(){window.MeDo.App.UpdateTask(taskId,taskBody,scheduledDate)}).toThrow(new window.MeDo.Error("ERROR_006"));
		
			
	});
	
	//removing tasks from the list
	it("Using an invalid taskId to remove a task throws an exceptoin",function(){
		var taskId = "Invalid Id";
		expect(function(){window.MeDo.App.RemoveTask(taskId);}).toThrow(new window.MeDo.Error("ERROR_006"));
	});
	
	it("We can remove task using task Id",function(){
		var newTaskBody = "Test Task";
		var testToDoItems = window.MeDo.TaskManager.GetTasks();
		var previousLength = testToDoItems.length;
		var newTaskScheduledDate = (new Date())+1;
		window.MeDo.App.addNewTask(newTaskBody,newTaskScheduledDate)
		expect(testToDoItems.length).toBe((previousLength+1));
		var taskId = testToDoItems.length;
		window.MeDo.App.RemoveTask(taskId);
		expect(testToDoItems.length).toBe(previousLength);
	});
	//adding new list 
	it("Trying to add a list with empty name throws an exception", function(){
		expect(function(){window.MeDo.App.AddNewList();}).toThrow(new window.MeDo.Error("ERROR_009"));
	});
	
	it("Trying to add already existing list throws an exception",function(){
		expect(function(){window.MeDo.App.AddNewList("home")}).toThrow(new window.MeDo.Error("ERROR_010"));
	});
	
	it("We can add new list by providing a valid name",function(){
		expect(window.MeDo.ListManager.GetListById("office_list")).toBe(-1);
		window.MeDo.App.AddNewList("Office List");
		expect(window.MeDo.ListManager.GetListById("office_list")).not.toBe(-1);
	});
	
	//removing an existing list
	it("Trying to remove a list without a list id throws an exception",function(){
		expect(function(){window.MeDo.App.RemoveList()}).toThrow(new window.MeDo.Error("ERROR_009"));
	});
	
	it("Trying to remove a non existing list throws an exception",function(){
		expect(function(){window.MeDo.App.RemoveList("invalid_id")}).toThrow(new window.MeDo.Error("ERROR_011"));
	});
	
	it("We can remove list by providing list id",function(){
		expect(window.MeDo.ListManager.GetListById("test_list")).toBe(-1);
		window.MeDo.App.AddNewList("Test List");
		expect(window.MeDo.ListManager.GetListById("test_list")).not.toBe(-1);
		window.MeDo.App.RemoveList("test_list");
		expect(window.MeDo.ListManager.GetListById("test_list")).toBe(-1);
	});
	
	//make a task belong to one or more listStyleType
	it("Trying to add a none existing task to list will throw an exception",function(){
		expect(function(){window.MeDo.App.AddToList()}).toThrow(new window.MeDo.Error("ERROR_006"));
	});
	
	it("Trying to add a task to none existing list will throw an exception",function(){
		expect(function(){window.MeDo.App.AddToList("1")}).toThrow(new window.MeDo.Error("ERROR_012"));
	});
	
	it("We can add a valid task to a valid list",function(){
		var testTask = window.MeDo.TaskManager.GetTaskById("1");
		expect(testTask.inLists.length).toBe(0);
		window.MeDo.App.AddToList("1","general");
		expect(testTask.inLists.length).toBe(1);
	});
	it("Trying to add the same task to a list will throw an exception",function(){
		expect(function(){window.MeDo.App.AddToList("1","general");}).toThrow(new window.MeDo.Error("ERROR_013"));
	});
	
	
	it("Trying to remove a none existing task from list will throw an exception",function(){
		expect(function(){window.MeDo.App.RemoveFromList()}).toThrow(new window.MeDo.Error("ERROR_006"));
	});
	
	it("Trying to remove none existing list from task will throw an exception",function(){
		expect(function(){window.MeDo.App.RemoveFromList("1")}).toThrow(new window.MeDo.Error("ERROR_012"));
	});
	
	
	it("We can remove an existing list from task.",function(){
		var testTask = window.MeDo.TaskManager.GetTaskById("1");
		expect(testTask.inLists.length).toBe(1);
		window.MeDo.App.RemoveFromList("1","general");
		expect(testTask.inLists.length).toBe(0);
	});
	
	it("Trying to remove a task that is not in the category throws an exception.",function(){
		expect(function(){window.MeDo.App.RemoveFromList("1","general")}).toThrow(new window.MeDo.Error("ERROR_014"));
		
	});
	
	//having no filters load the full task list
	it("Having empty filters will return the full list of tasks.",function(){
		expect(window.MeDo.App.GetTaskList({}).length).toBe(window.MeDo.TaskManager.GetTasks().length);
		expect(window.MeDo.App.GetTaskList().length).toBe(window.MeDo.TaskManager.GetTasks().length);
	});
	
	//setting the completed/not completed status will result in loading completed or not completd tasks
	it("Setting completed/not completed status will result in filtering by status.",function(){
		
		spyOn(window.MeDo.TaskManager,"GetTasks").and.returnValue(testToDoItems);
		
		var completedObj = {"Completed":true};
		var notCompletedObj = {"Completed": false};
		var completedFilteredList = window.MeDo.App.GetTaskList(completedObj);
		var notCompletedFilteredList = window.MeDo.App.GetTaskList(notCompletedObj)
		//ensure our spy is working
		expect(window.MeDo.TaskManager.GetTasks).toHaveBeenCalled()
		expect(completedFilteredList.length).toBe(2);
		expect(notCompletedFilteredList.length).toBe(3);
	});
	
	//setting active list will result in only tasks in that list
	it("Setting active list will return objects belonging to that list.",function(){
		
		spyOn(window.MeDo.TaskManager,"GetTasks").and.returnValue(testToDoItems);
		
		var generalListObj = {"ActiveList":"general"};
		var homeListObj = {"ActiveList": "home"};
		var generalFilteredList = window.MeDo.App.GetTaskList(generalListObj);
		var homeFilteredList = window.MeDo.App.GetTaskList(homeListObj)
		//ensure our spy is working
		expect(window.MeDo.TaskManager.GetTasks).toHaveBeenCalled()
		expect(generalFilteredList.length).toBe(3);
		expect(homeFilteredList.length).toBe(3);
	});
	
	//setting active date/range will only load tasks within that scheduled date period
	it("Setting active date/range will return objects belonging to that list.",function(){
		
		spyOn(window.MeDo.TaskManager,"GetTasks").and.returnValue(testToDoItems);
		
		var firstActiveDateRangeFilter = {"ScheduleDateRange":{
				"From" : new Date(2016,2,25),
				"To" : new Date(2016,2,26)
			}
		};
		
		var secondActiveDateRangeFilter = {"ScheduleDateRange":{
				"From" : new Date(2016,2,26),
				"To" : new Date(2016,2,28)
			}
		};
		
		var firstDateRangeFilteredList = window.MeDo.App.GetTaskList(firstActiveDateRangeFilter);
		var secondDateRangeFilteredList = window.MeDo.App.GetTaskList(secondActiveDateRangeFilter)
		//ensure our spy is working
		expect(window.MeDo.TaskManager.GetTasks).toHaveBeenCalled()
		expect(firstDateRangeFilteredList.length).toBe(3);
		expect(secondDateRangeFilteredList.length).toBe(4);
	});
	
	//filtering by taskbody will result in loading only task that matches text pattern
	it("filtering by taskbody will return objects belonging to that list.",function(){
		
		spyOn(window.MeDo.TaskManager,"GetTasks").and.returnValue(testToDoItems);
		
		var firstTaskBodyFilter = {"TaskBody": "Buy" };
		
		var secondTaskBodyFilter = {"TaskBody": "Andre"	};
		
		var firstTaskBodyFilteredList = window.MeDo.App.GetTaskList(firstTaskBodyFilter);
		var secondTaskBodyFilteredList = window.MeDo.App.GetTaskList(secondTaskBodyFilter)
		//ensure our spy is working
		expect(window.MeDo.TaskManager.GetTasks).toHaveBeenCalled()
		expect(firstTaskBodyFilteredList.length).toBe(3);
		expect(secondTaskBodyFilteredList.length).toBe(1);
	});
	
	//tasks that are displyed can be filterd by completed/not completed, date/range, text body and by list 
	it("multiple filters can be given for the filtering",function(){
		
		spyOn(window.MeDo.TaskManager,"GetTasks").and.returnValue(testToDoItems);
		
		var firstFilter = {
			"ScheduleDateRange":{
						"From" : new Date(2016,2,26),
						"To" : new Date(2016,2,28)
					},
			"TaskBody": "Buy",
			"ActiveList": "personal",
			"Completed": false
		};
		
				
		var firstFilteredList = window.MeDo.App.GetTaskList(firstFilter);
		
		//ensure our spy is working
		expect(window.MeDo.TaskManager.GetTasks).toHaveBeenCalled()
		expect(firstFilteredList.length).toBe(2);
		
	});
});