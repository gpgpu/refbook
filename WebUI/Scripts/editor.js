/// <reference path="../jquery-1.4.4-vsdoc.js" />

function ApplyBlockStyle(styleName) {
    // get the anchor for selected text
    var anchor = window.getSelection().anchorNode.parentNode;

    var blockanchor = anchor;

    while (blockanchor.nodeName != "BODY" && blockanchor.nodeName != "P" && blockanchor.nodeName != "H1" && blockanchor.nodeName != "H2" && blockanchor.nodeName != "H3") {
        blockanchor = blockanchor.parentNode;
    }

    if (blockanchor.nodeName != "BODY") {
        var c = document.createElement(styleName);
        c.innerHTML = blockanchor.innerHTML;
        blockanchor.parentNode.replaceChild(c, blockanchor);
    }

    return false;
}

function editStyle(cmd) {
    document.execCommand(cmd, null, null);
}

function insertTag(tagName) {

    if (window.getSelection) {
        var sel = window.getSelection();
        if (sel.getRangeAt && sel.rangeCount) {
            var range = sel.getRangeAt(0);
            switch (tagName) {
                case 'pre':
                    var selectionContents = range.extractContents();
                    var theElement = document.createElement("pre");
                    theElement.appendChild(selectionContents);
                    range.insertNode(theElement);
                    break;
                case 'ul':
                    var selectionContents = range.extractContents();
                    var ulElement = document.createElement("ul");
                    var liElement = document.createElement("li");
                    liElement.appendChild(selectionContents);
                    ulElement.appendChild(liElement);
                    range.insertNode(ulElement);
                    break;
                case 'ol':
                    var selectionContents = range.extractContents();
                    var olElement = document.createElement("ol");
                    var liElement = document.createElement("li");
                    liElement.appendChild(selectionContents);
                    olElement.appendChild(liElement);
                    range.insertNode(olElement);
                    break;
                default:
                    range.deleteContents();
                    range.insertNode(document.createElement(tagName));
                    break;
            }
        }
    }
}

// imageId: for deleting purpose
function insertImg(range, src, imageId) {
    if (range) {
        range.deleteContents();
        var divContainer = document.createElement("div");
        divContainer.setAttribute('class', 'divImageHolder');

        var img = document.createElement("img");
        img.setAttribute('src', src);
        img.setAttribute('alt', 'img');
        img.setAttribute('id', imageId);

        divContainer.appendChild(img);

        range.deleteContents();
        range.insertNode(divContainer);

    }
}

