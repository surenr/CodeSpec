(function(common){
'use strict';

var myLists = [
		{Id:"general", Name: "General",Color: "#C63D0F"},
		{Id:"home", Name: "Home",Color: "#3B3738"},
		{Id:"personal", Name: "Personal",Color: "#7E8F7C"}
	];

var myTestList = [
		{Id:"general", Name: "General",Color: "#C63D0F"},
		{Id:"home", Name: "Home",Color: "#3B3738"},
		{Id:"personal", Name: "Personal",Color: "#7E8F7C"}
	];



if(!common.MeDo) common.MeDo = {};	

var listManager = {};
listManager.GetList = function(){
	return myLists || myTestList;
};

listManager.GetListById = function(listId){
	return _.findIndex(this.GetList(),{Id: listId});
};

listManager.AddList  = function(listObj){
	if(!listObj || !listObj.Id || !listObj.Name || !listObj.Color) throw new window.MeDo.Error("ERROR_012");
	var list = this.GetList();
	list[list.length] = listObj;
};

listManager.RemoveList = function(objectToRemove){
	if(!objectToRemove || !objectToRemove.Id) throw new window.MeDo.Error("ERROR_012");
	var index = this.GetListById(objectToRemove.Id);
	if(index==-1) throw new window.MeDo.Error("ERROR_011");
	this.GetList().splice(index,1);
};

common.MeDo.ListManager = listManager;
	
}(window));
