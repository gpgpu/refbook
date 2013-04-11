var refBookApp = angular.module('refBookApp', []);



var ContentItem = function (originalItem, name, displayOrder) {
    var self = this;
    self.name = name;
    self.originalItem = originalItem;
    self.displayOrder = displayOrder;
    
};
ContentItem.prototype.clickItem = function () {
    alert(this.name);
};

var Book = function () {
    var self = this;
    self.title = '';
    self.expanded = false;
    self.topics = [];
    
};
Book.prototype.toggleExpansion = function () {
    this.expanded = !(this.expanded);
};
Book.prototype.handleIndicator = function () {
    if (this.topics.length == 0) return ' ';
    if (this.expanded) return '-';
    return '+';
};

var Topic = function (topicId, bookId, parent, title, displayOrder) {
    var self = this;

    self.id = topicId;
    self.bookId = bookId;
    self.parent = parent;
    self.title = title;
    self.displayOrder = displayOrder;

    self.expanded = false;
    self.isSelected = false;
    self.topics = [];
    self.articles = [];

    
};
Topic.prototype.hasChildren = function () {
    return this.topics && this.topics.length > 0;
};
Topic.prototype.toggleExpansion = function () {
    this.expanded = !(this.expanded);
};
Topic.prototype.handleIndicator = function () {
    if (!this.hasChildren()) return ' ';
    if (this.expanded) return '-';
    return '+';
};
Topic.prototype.isTopic = true;

var Article = function (parent, articleId, title, displayOrder) {
    var self = this;
    self.parent = parent;
    self.id = articleId;
    self.title = title;
    self.displayOrder = displayOrder;
};
Article.prototype.isTopic = false;


function parseTopic(serverTopic) {
    var clientTopic = new Topic(
        serverTopic.TopicId,
        serverTopic.BookId,
        null,
        serverTopic.Title,
        serverTopic.DisplayOrder
        );
    
    if (serverTopic.Articles && serverTopic.Articles.length) {
        for (var articleIndex = 0; articleIndex < serverTopic.Articles.length; articleIndex++) {
            var serverArticle = serverTopic.Articles[articleIndex];
            var clientArticle = new Article(
                clientTopic,
                serverArticle.ArticleId,
                serverArticle.Title,
                serverArticle.DisplayOrder);
            clientTopic.articles.push(clientArticle);
        }
    }

    if (serverTopic.Topics) {
        for (var k = 0; k < serverTopic.Topics.length; k++) {
            var serverChildTopic = serverTopic.Topics[k];
            var childTopic = parseTopic(serverChildTopic);
            childTopic.parent = clientTopic;
            clientTopic.topics.push(childTopic);
        }
    }

    return clientTopic;
}

function manager($scope, $http, $timeout) {
    
    $http.jsonp("http://local.refbookservice.org/api/book/GetTOB/1?callback=JSON_CALLBACK").success(function (data) {
        $scope.portal = data.Title;
        $scope.books = [];
        for (var index = 0; index < data.Books.length; index++) {
            var book = new Book();
            var serverBook = data.Books[index];
            book.title = serverBook.Title;
            if (serverBook.Topics && serverBook.Topics.length > 0) {
                for (var j = 0; j < serverBook.Topics.length; j++) {
                    var serverTopic = serverBook.Topics[j];
                    var clientTopic = parseTopic(serverTopic);
                    clientTopic.parent = book;

                    book.topics.push(clientTopic);
                }


                $scope.books.push(book);
            }
        }
        
    });

    $scope.selectedItem = {};
    $scope.itemSorted = false;

    $scope.select = function (newSelect) {
        if ($scope.selectedItem.isSelected) {
            $scope.selectedItem.isSelected = false;

        }
        $scope.selectedItem = newSelect;
        $scope.selectedItem.isSelected = true;
        $scope.rightItems = newSelect.topics.concat(newSelect.articles);
        $scope.rightItems.sort(function (a, b) { return a.displayOrder - b.displayOrder; });

       

    };
    
    $scope.rightItems = [ ];
   
    $scope.rightSelect = function (newSelect) {
        if (newSelect instanceof Topic) {
            $scope.select(newSelect);
            $scope.expandAllParents(newSelect);
        }       
    };

   
    $scope.expandAllParents = function (item) {
        if (item.parent) {
            item.parent.expanded = true;
            $scope.expandAllParents(item.parent);
        }
    };

    $scope.cancelSort = function(){
        $scope.itemSorted = false;
    };
    $scope.saveOrder = function () {
        $scope.rightItems.forEach(function (item, index) {
            item.displayOrder = index;
        });
        
        var para = $scope.rightItems.map(function (x) {
            return {"id": x.id, "isTopic": x.isTopic };
        });

        $http.post("/portal/1/Book/SaveItemsOrder", para).success(function () {
            $scope.itemSorted = false;
            $scope.selectedItem.topics.sort(function (a, b) {
                return a.displayOrder - b.displayOrder;
            });
        });

       
    };

    $scope.showAddFilePanel = function () {

    };
}

refBookApp.directive('ngRightItems', function () {
    return function (scope, element, attrs) {

        var toUpdate;
        var startIndex = -1;

        // watch the model, so we always know what element
        // is at a specific position
        //scope.$watch(attrs.ngSortable, function (value) {
        //    //  toUpdate = value;
        //    toUpdate = scope.rightItems;
        //}, true);

        // use jquery to make the element sortable. This is called
        // when the element is rendered
        
        $(element[0]).sortable({
            items: 'div',
            start: function (event, ui) {
                // on start we define where the item is dragged from
                startIndex = ($(ui.item).index());
            },
            stop: function (event, ui) {
                // on stop we determine the new index of the
                // item and store it there
                
                toUpdate = scope.rightItems;
                var newIndex = ($(ui.item).index());
                var toMove = toUpdate[startIndex];
                toUpdate.splice(startIndex, 1);
                toUpdate.splice(newIndex, 0, toMove);

                scope.$apply(function () {
                   
                    scope.itemSorted = true;
                });
               
            }
        })
    };
});
refBookApp.directive('droppableTopic', function () {
    return function (scope, element, attr) {
        angular.element(element).droppable({
            greedy: true,
            hoverClass: 'dropping',
            accept: '.articleRow, .topicRow',
            
            drop: function (event, ui) {
            var dropTarget = angular.element(this);
            var index = ui.draggable.index();
            var theItem = scope.rightItems[index];
            console.log(theItem.title);
           
        }
        });
    }
});
refBookApp.directive('droppableBook', function () {
    return function (scope, element, attr) {
        angular.element(element).droppable({
            greedy: true,
            hoverClass: 'dropping',
            accept: '.topicRow',
            drop: function (event, ui) {
                var dropTarget = angular.element(this);
                //    console.log(attr['id']);
                //    console.log(attr['type']);
                var index = ui.draggable.index();
                var draggedTopic = scope.rightItems[index];
                console.log(draggedTopic.name);

            }
            
        });
    }
});
