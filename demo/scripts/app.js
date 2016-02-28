(function(common){
'use strict';

//random colors
var randomColors = ["#C63D0F","#3B3738","#7E8F7C","#005A31","#A8CD1B","#CBE32D","#F3FAB6","#558C89","#74AFAD","#D9853B","#DE1B1B","#7D1935","#4A96AD","#E44424","#FF9009","#DF3D82"]
var getRandomColor  = function(){
	return randomColors[Math.floor(Math.random() * randomColors.length)];
};
	
	var app = {};
	app.taskList = [];
	app.addNewTask = function(taskBody,taskDateTime){
		var getTaskList = common.MeDo.TaskManager.GetTasks();
		common.MeDo.Validator.validateNewTaskCreation(getTaskList,taskBody,taskDateTime);
		var newTask = {
			Id: common.MeDo.TaskManager.GeNextTaskId(),
			  scheduledDate: taskDateTime,
			  addedDate: new Date(),
			  body: taskBody,
			  inLists: [],
			  isCompleted: false
		}
		common.MeDo.TaskManager.addTask(newTask);
	};
	
	app.CompleteTask = function(taskId){
		var objectToUpdate = {
			Id : taskId,
			isCompleted : true
		};
		common.MeDo.TaskManager.UpdateTask(objectToUpdate);
	};
	
	app.ReOpenTask = function(taskId){
		var objectToUpdate = {
			Id : taskId,
			isCompleted : false
		};
		common.MeDo.TaskManager.UpdateTask(objectToUpdate);
	};
	
	app.UpdateTask = function(taskId,taskBody,scheduledDate){
		
		if(!taskBody) throw new common.MeDo.Error("ERROR_007");
		if(scheduledDate && scheduledDate<(new Date())) throw new common.MeDo.Error("ERROR_008");
		var objectToUpdate = {
			Id : taskId,
			body : taskBody
		};
		
		if(scheduledDate)
			objectToUpdate.scheduledDate = scheduledDate;
		common.MeDo.TaskManager.UpdateTask(objectToUpdate);
		
	};
	
	app.RemoveTask = function(taskId){
		var objectToDelete = {
			Id: ""+taskId+""
		};
		common.MeDo.TaskManager.RemoveTask(objectToDelete);
	};
	
	app.AddNewList = function(listName){
		if(!listName) throw new common.MeDo.Error("ERROR_009");
		var newId = listName.toLowerCase().replace(" ","_");
		if(common.MeDo.ListManager.GetListById(newId)>-1) throw new common.MeDo.Error("ERROR_010");
		var newObj = {Id:newId, Name: listName,Color: getRandomColor()};
		common.MeDo.ListManager.AddList(newObj);
	};
	
	app.RemoveList  = function(listName){
		if(!listName) throw new common.MeDo.Error("ERROR_009");
		var objectToRemove = {Id:listName};
		common.MeDo.ListManager.RemoveList(objectToRemove);
	};
	
	app.AddToList = function(taskId, listId){
		var task = common.MeDo.TaskManager.GetTaskById(taskId);
		var listIndex = common.MeDo.ListManager.GetListById(listId);
		
		if(!task) throw new common.MeDo.Error("ERROR_006");
		if(listIndex==-1) throw new common.MeDo.Error("ERROR_012");
		
		var existingIndex = _.indexOf(task.inLists,listIndex);
		if(existingIndex>-1) throw new common.MeDo.Error("ERROR_013");
		
		task.inLists[task.inLists.length] = listIndex;
	};
	
	app.RemoveFromList = function(taskId,listId){
		var task = common.MeDo.TaskManager.GetTaskById(taskId);
		var listIndex = common.MeDo.ListManager.GetListById(listId);
		
		if(!task) throw new common.MeDo.Error("ERROR_006");
		if(listIndex==-1) throw new common.MeDo.Error("ERROR_012");	
		var existingIndex = _.indexOf(task.inLists,listIndex);
		if(existingIndex==-1) throw new common.MeDo.Error("ERROR_014");
		task.inLists.splice(existingIndex,1);
	};
	
	app.GetTaskList = function(filter){
		if(!filter || Object.keys(filter).length==0) return common.MeDo.TaskManager.GetTasks();
		return _.filter(common.MeDo.TaskManager.GetTasks(),function(object){
			var returnBool = true;
			if(filter["Completed"])
				returnBool = returnBool && object.isCompleted==filter["Completed"];
		});
	};
	if(!common.MeDo) common.MeDo = {};	
	common.MeDo.App = app;
}(window));
