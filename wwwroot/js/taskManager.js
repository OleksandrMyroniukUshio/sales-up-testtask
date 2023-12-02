function loadTasks(sortBy, sortOrder) {
    var url = $('#tasksList').data('get-tasks-url');
    $.get(url, { sortBy: sortBy, descending: (sortOrder === "desc") }, function (tasks) {
        $('#tasksList').empty();
         tasks.forEach(function (task) {
            var checkedAttr = task.isCompleted ? 'checked' : '';
            var completedStyle = task.isCompleted ? 'completed-task' : '';
            var row = $('<div class="row align-items-center">' +
                '<div class="col-auto">' +
                '<input type="checkbox" class="task-completion" data-id="' + task.id + '" ' + checkedAttr + ' />' +
                '</div>' +
                '<div class="col task-text ' + completedStyle + '">' + task.name + '</div>' +
                '<div class="col-auto">' +
                '<button class="btn btn-danger btn-delete-task" data-id="' + task.id + '">' +
                '<i class="fa fa-trash"></i>' +
                '</button>' +
                '</div>' +
                '<div class="col-auto">' +
                '<button class="btn btn-primary btn-edit-task" data-id="' + task.id + '">' +
                '<i class="fa fa-pencil"></i> Edit' +
                '</button>' +
                '</div>' +
                '</div>');
            $('#tasksList').append(row);
        });

       
    });
}

function addTask(taskName) {

    if (taskName.length > 120) {
        alert("Task name cannot be more than 120 characters");
        return;
    }
    var url = $('#tasksList').data('add-task-url');
    $.ajax({
        type: "POST",
        url: url,
        contentType: "application/json",
        data: JSON.stringify({ name: taskName }),
        success: function () {
            loadTasks();
        },
        error: function () {
            alert("Error adding task");
        }
    });
}

function toggleCompletion(taskId, taskElement) {
    var baseUrl = $('#tasksList').data('toggle-completion-url');
    var url = baseUrl + '/' + taskId; 

    $.ajax({
        type: "PATCH",
        url: url,
        success: function () {
            taskElement.toggleClass('completed-task', !taskElement.hasClass('completed-task'));
        },
        error: function () {
            alert("Error toggling task completion");
        }
    });
}

// Delete a task
function deleteTask(taskId) {
    var baseUrl = $('#tasksList').data('delete-task-url');
    var url = baseUrl + '/' + taskId;

    $.ajax({
        type: "DELETE",
        url: url,
        success: function () {
            loadTasks();
        },
        error: function () {
            alert("Error deleting task");
        }
    });
}
function updateTaskName(taskId, newName) {
    var url = $('#tasksList').data('update-task-name-url') + '/' + taskId;
    $.ajax({
        type: "PATCH",
        url: url,
        contentType: "application/json",
        data: JSON.stringify(newName),
        success: function () {
            loadTasks();  
        },
        error: function () {
            alert("Couldn't update the name");
        }
    });
}