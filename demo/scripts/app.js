(function(){
'use strict';

//random colors
var randomColors = ["#C63D0F","#3B3738","#7E8F7C","#005A31","#A8CD1B","#CBE32D","#F3FAB6","#558C89","#74AFAD","#D9853B","#DE1B1B","#7D1935","#4A96AD","#E44424","#FF9009","#DF3D82"]
var myLists = [{Id:"general", Name: "General",Color: "#C63D0F"},{Id:"home", Name: "Home",Color: "#3B3738"},{Id:"personal", Name: "Personal",Color: "#7E8F7C"}];

var testToDoItems = [
  {
      Id: "1",
      addedDate: new Date("2016-02-05T11:20:00"),
      body: 'Talk to Andre and schedule a meeting for Monday.',
      inLists: [],
      isCompleted: true
  },
  {
      Id: "2",
      addedDate: new Date("2016-02-05T11:30:00"),
      body: 'Buy medecin for Thinithi',
      inLists: [],
      isCompleted: true
  },
  {
      Id: "3",
      addedDate: new Date("2016-02-06T10:30:00"),
      body: 'Do Exersises',
      inLists: [],
      isCompleted: false
  },
  {
      Id: "4",
      addedDate: new Date("2016-02-06T11:30:00"),
      body: 'Code Review all pending tasks',
      inLists: [],
      isCompleted: false
  },
  {
      Id: "5",
      addedDate: new Date("2016-02-06T11:30:00"),
      body: 'Buy gift fro wife',
      inLists: [],
      isCompleted: false
  }

];
var ownerListAllowDrop =  function(ev) {
    ev.preventDefault();
	};

var ownerListDrag =  function(ev) {
		ev.dataTransfer.setData("text", ev.target.id);
	};

var ownerListDropped =  function(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
	var scope = angular.element($("#appCtrl")).scope();
	var alreadyDropped = false;
    scope.$apply(function(){
        var taskId = ev.target.id.replace("task_","");
		var i = 0;var j=0;
		for(;i<scope.todoitems.length;i++){
			if(scope.todoitems[i].Id==taskId){
				for(j=0;j<myLists.length;j++){
					if(myLists[j].Id==data){
						alreadyDropped = false;
						for(var k=0;k<scope.todoitems[i].inLists.length;k++){
							if(scope.todoitems[i].inLists[k]==j){
								alreadyDropped=true;
							}
						}
						if(!alreadyDropped)
							scope.todoitems[i].inLists[scope.todoitems[i].inLists.length] = j;
					}
				}
				
			}
		}
    });
};

window.ownerListAllowDrop = new Function();
window.ownerListDrag = new Function();
window.ownerListDropped = new Function();
window.ownerListAllowDrop = ownerListAllowDrop;
window.ownerListDrag = ownerListDrag;
window.ownerListDropped = ownerListDropped;

var appLogic = {};

appLogic.getRandomColor = function(){
	return randomColors[Math.floor(Math.random() * randomColors.length)];
};

appLogic.removeToDoItem = function(todoItemList,itemId){
  if(todoItemList && itemId){
    var i=0; var itemToDeleteIndex = -1;
    for(;i<todoItemList.length;i++){
      if(todoItemList[i].Id==itemId){
        itemToDeleteIndex = i;
        break;
      }
    }
    if(itemToDeleteIndex!=-1){
      var confirmDelete = confirm("Are you sure you want to delete Item \""+todoItemList[i].body+"\"");
      if(confirmDelete){
          todoItemList.splice(itemToDeleteIndex,1);
      }
    }
  }
};

appLogic.reOpenToDoItem = function(todoItemList,itemId){
  if(todoItemList && itemId){
    var i=0;
    for(;i<todoItemList.length;i++){
      if(todoItemList[i].Id==itemId){
        todoItemList[i].isCompleted = false;
        break;
      }
    }
  }
  return todoItemList;
};

appLogic.completeToDoItem = function(todoItemList,itemId){
  if(todoItemList && itemId){
    var i=0;
    for(;i<todoItemList.length;i++){
      if(todoItemList[i].Id==itemId){
        todoItemList[i].isCompleted = true;
        break;
      }
    }
  }
};
appLogic.addToDoItem = function(todoItemList,body){
  if(todoItemList && body){
    var nextItemId = todoItemList.length + 1;
    var i=0;
    for(;i<todoItemList.length;i++){
      if(todoItemList[i].Id==nextItemId.toString()){
        i=0; nextItemId++; continue;
      }
    }
    todoItemList.unshift({
      Id: nextItemId.toString(),
      addedDate: new Date(),
      body: body,
      inLists: ["General"],
      isCompleted: false
    });
  }
};

appLogic.editToDoItem = function(todoItemList,editItemId,newBody){
  if(todoItemList && editItemId, newBody){
    var i=0;
    for(;i<todoItemList.length;i++){
      if(todoItemList[i].Id==editItemId){
        todoItemList[i].body = newBody;
        break;
      }
    }
  }
};

//angulaer code start here
var app = angular.module('app',[]);
app.controller('PostsCtrl',function($scope,$location,$anchorScroll){
  var addItem = function(){
    appLogic.addToDoItem($scope.todoitems,$scope.itemBody);
    $scope.itemBody = null;
  };

	
 
  
  $scope.reOpenTask = function(selectedItemId){

    $scope.todoitems = appLogic.reOpenToDoItem($scope.todoitems,selectedItemId);
  };
  $scope.completeTask = function(selectedItemId){
    appLogic.completeToDoItem($scope.todoitems,selectedItemId);
  };
  $scope.removeTask = function(selectedItemId){
    appLogic.removeToDoItem($scope.todoitems,selectedItemId);
  };
  $scope.editTask = function(selectedItemId){
    $scope.doAction = function(){
        appLogic.editToDoItem($scope.todoitems,selectedItemId,$scope.itemBody);
        $scope.doAction = addItem;
        $("#btnAddItem").html("Add Item");
        $scope.itemBody = null;
    };

    $("#btnAddItem").html("Update Item");
    var i=0;
    for(;i<$scope.todoitems.length;i++){
      if($scope.todoitems[i].Id==selectedItemId){
        $scope.itemBody = $scope.todoitems[i].body;
      }
    }
    $location.hash("itemBodyLoc");
    $anchorScroll()
    $("#txtItemBody").focus();
  };

  $scope.ownerListRemove = function(taskObj,inListIndex){
	 
	 if(taskObj && inListIndex!='undefined'){
		 for(var i=0;i<taskObj.inLists.length;i++){
			 if(taskObj.inLists[i]==inListIndex){
				 taskObj.inLists.splice(i,1);
				 break;
			 }
		 }
	 } 
  };
  
  $scope.addNewList = function(){
	var newName = prompt("Please enter a name for the new list");
	var isExisting = false;
	if(newName){
		for(var i=0;i<myLists.length;i++){
			if(myLists[i].Name.toUpperCase()===newName.toUpperCase()){
				isExisting=true;
				break;
			}
				
		}
		if(!isExisting){
			myLists[myLists.length] = {Id:newName.toLowerCase().replace(" ",""), Name: newName,Color: appLogic.getRandomColor()};
		}
		else {
			alert("List with name \""+newName+"\" already exists");
		}
	}
  };
  
  $scope.RemoveList = function(myListId){
	 
	for(var i=0;i<myLists.length;i++){
		if(myLists[i].Id.toUpperCase()===myListId.toUpperCase()){
			if(confirm("Are you sure to delete the todo list \""+myLists[i].Name+"\"?")){
				myLists.splice(i,1);
				break;
			 }
		}
			
	}
	  
  };
  
  $scope.doAction = addItem;
  $scope.todoitems = testToDoItems;
  $scope.myTodoLists = myLists;
});

}());
