(function(common){
'use strict';

	var errorMap = {
		"ERROR_001": "Task list is empty.",
		"ERROR_002": "Are you trying to add an empty task? You must enter a task description",
		"ERROR_003": "Hm...Seems your task doesn't have a scheduled date.",
		"ERROR_004": "You can only add ToDo Tasks, Please select a date/time in the future.",
		"ERROR_005": "Task you try to add already exists.",
		"ERROR_006": "Task you are trying to access, doesn't exists.",
		"ERROR_007": "Removing the task description while editing it is not allowed.",
		"ERROR_008": "Sorry, schduled date should be of a future date.",
		"ERROR_009": "List must have a name.",
		"ERROR_010": "List already exists.",
		"ERROR_011": "List you are trying to remove does not exist.",
		"ERROR_012": "List you are trying to access doesn't exists.",
		"ERROR_013": "Task already belong to the list",
		"ERROR_014": "Task is not in the category"
	};
	if(!common.MeDo) common.MeDo = {};	
	common.MeDo.Error = function(errorCode){
		this.name = errorCode || "-1",
		this.message = errorMap[errorCode] || "unexpected error";
		this.stack = (new Error()).stack;
	};
	
	common.MeDo.Error.prototype = Object.create(Error.prototype);
	common.MeDo.Error.prototype.constructor = common.MeDo.Error;
}(window));
