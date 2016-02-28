(function(common){
'use strict';

	
	var testToDoItems = [
	  {
		  Id: "1",
		  addedDate: new Date("2016-02-05T11:20:00"),
		  scheduledDate: new Date(2016,2,25),
		  body: 'Talk to Andre and schedule a meeting for Monday.',
		  inLists: [],
		  isCompleted: true
	  },
	  {
		  Id: "2",
		  addedDate: new Date("2016-02-05T11:30:00"),
		  scheduledDate: new Date(2016,2,26),
		  body: 'Buy medecin for Thinithi',
		  inLists: [],
		  isCompleted: true
	  },
	  {
		  Id: "3",
		  addedDate: new Date("2016-02-06T10:30:00"),
		  scheduledDate: new Date(2016,2,26),
		  body: 'Do Exersises',
		  inLists: [],
		  isCompleted: false
	  },
	  {
		  Id: "4",
		  addedDate: new Date("2016-02-06T11:30:00"),
		  scheduledDate: new Date(2016,2,27),
		  body: 'Code Review all pending tasks',
		  inLists: [],
		  isCompleted: false
	  },
	  {
		  Id: "5",
		  addedDate: new Date("2016-02-06T11:30:00"),
		  scheduledDate: new Date(2016,2,28),
		  body: 'Buy gift fro wife',
		  inLists: [],
		  isCompleted: false
	  }

	];

	
	var taskManager = {};
	taskManager.taskList = testToDoItems.slice();
	taskManager.GetTasks = function(){
		return this.taskList;
	};
	taskManager.addTask = function(taskObj){
		if(_.find(this.GetTasks(),function(obj){return taskObj.Id==obj.Id;}))
			throw new common.MeDo.Error("ERROR_005");
		var taskList = this.GetTasks();
		taskList[taskList.length] = taskObj;
	};
	
	taskManager.GeNextTaskId = function(){
		return (this.GetTasks().length + 1).toString();
	};
	
	taskManager.GetTaskById = function(id){
		return _.find(this.GetTasks(),function(obj){return id==obj.Id;});
	};
	
	taskManager.UpdateTask  = function(objectToUpdate){
		var objectFromList = this.GetTaskById(objectToUpdate.Id);
		if(!objectFromList) throw new common.MeDo.Error("ERROR_006");
		
		if(objectToUpdate.addedDate)
			objectFromList.addedDate = objectToUpdate.addedDate;
		if(objectToUpdate.scheduledDate)
			objectFromList.scheduledDate = objectToUpdate.scheduledDate;
		if(objectToUpdate.body)
			objectFromList.body = objectToUpdate.body;
		if(objectToUpdate.inLists)
			objectFromList.inLists = objectToUpdate.inLists;
		if(objectToUpdate.isCompleted!=undefined)
			objectFromList.isCompleted = objectToUpdate.isCompleted;
		
	}
	
	taskManager.RemoveTask = function(objectToDelete){
		var deleteTaskIndex = _.findIndex(this.GetTasks(),objectToDelete);
		if(deleteTaskIndex>-1){
			this.GetTasks().splice(deleteTaskIndex,1);
		}
		else
			throw new common.MeDo.Error("ERROR_006");
	};
	
	if(!common.MeDo) common.MeDo = {};
	common.MeDo.TaskManager = taskManager;
	
}(window));
