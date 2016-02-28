(function(common){
'use strict';

	var validator = {
		validateNewTaskCreation: function(taskList,taskBody,taskDateTime){
			if(taskList.length==0) throw new common.MeDo.Error("ERROR_001");
			if(!taskBody) throw new common.MeDo.Error("ERROR_002");
			if(taskDateTime==null || !taskDateTime instanceof Date) throw new common.MeDo.Error("ERROR_003");
			var today = new Date();
			if(taskDateTime<today) throw new common.MeDo.Error("ERROR_004");
		}
	};
	if(!common.MeDo) common.MeDo = {};
	common.MeDo.Validator = validator;
}(window));
