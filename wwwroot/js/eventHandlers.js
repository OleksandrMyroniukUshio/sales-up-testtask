$(document).ready(function () {

    var defaultSortType = 'Name';
    $('.sort-btn button:first').data('sort-type', defaultSortType).text(defaultSortType);
    loadTasks(defaultSortType, 'asc');

    $('.sort-btn .dropdown-menu a').on('click', function (e) {
        e.preventDefault();
        var sortType = $(this).data('sort');
        var sortOrder = $('#toggleSortOrderBtn').data('sort-order');
        loadTasks(sortType, sortOrder);

        var sortTypeText = $(this).text();
        $('.sort-btn button:first').text(sortTypeText).data('sort-type', sortType);
    });
    $('#toggleSortOrderBtn').on('click', function () {
        var currentOrder = $(this).data('sort-order');
        var newOrder = currentOrder === 'asc' ? 'desc' : 'asc';
        $(this).data('sort-order', newOrder)
            .find('i').toggleClass('fa-sort-amount-asc fa-sort-amount-desc');

        var sortType = $('.sort-btn button:first').data('sort-type');
        loadTasks(sortType, newOrder);
    });
    $('#addTaskBtn').on('click', function () {
        var taskName = $('#newTaskName').val();
        if (taskName) {
            addTask(taskName);
            $('#newTaskName').val('');
        }
    });

    $(document).on('change', '.task-completion', function () {
        var taskId = $(this).data('id');
        toggleCompletion(taskId, $(this).parent().next());
    });

    $(document).on('click', '.btn-delete-task', function () {
        var taskId = $(this).data('id');
        deleteTask(taskId);
    });

    // Event delegation for dynamically added edit task buttons
    $(document).on('click', '.btn-edit-task', function () {
        var taskId = $(this).data('id');
        var taskRow = $(this).closest('.row');
        var taskElement = taskRow.find('.task-text');
        var taskName = taskElement.text();

        // Hide the checkbox and replace task text with an input field
        taskRow.find('.task-completion').hide();
        taskElement.replaceWith('<input type="text" class="form-control task-edit-field" placeholder="Update task name" value="' + taskName + '" />');
        $(this).replaceWith('<button class="btn btn-success btn-confirm-edit" data-id="' + taskId + '">Confirm</button>');
    });

    // Event delegation for dynamically added confirm edit buttons
    $(document).on('click', '.btn-confirm-edit', function () {
        var taskId = $(this).data('id');
        var taskRow = $(this).closest('.row');
        var editedName = taskRow.find('.task-edit-field').val();
        taskRow.find('.task-completion').show();
        updateTaskName(taskId, editedName, function (success) {
            if (success) {
                taskRow.find('.task-edit-field').replaceWith('<div class="col task-text">' + editedName + '</div>');
                $(this).replaceWith('<button class="btn btn-primary btn-edit-task" data-id="' + taskId + '"><i class="fa fa-pencil"></i> Edit</button>');
            } else {
                alert("Error updating task name");
            }
        });
    });
});
