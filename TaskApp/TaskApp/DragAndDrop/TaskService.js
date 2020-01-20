class TaskService {
  storageKey = 'TASKS';

  tasksData = [
    {
      id: 1,
      title: 'Client meeting',
      status: 'todo'
    },
    {
      id: 2,
      title: 'Plan webinar',
      status: 'todo'
    },
    {
      id: 3,
      title: 'Email newsletter',
      status: 'todo'
    },
    {
      id: 4,
      title: 'Task \'Drag&Drop\'',
      status: 'inprogress'
    },
    {
      id: 5,
      title: 'Publish podcast',
      status: 'done'
    },
    {
      id: 6,
      title: 'Launch website',
      status: 'done'
    }
    //todo:count  =  20 
  ];

  getTasks() {
    const jsonTasks = localStorage.getItem(this.storageKey);
    if (jsonTasks !== null)
      this.tasksData = JSON.parse(jsonTasks);

    return this.tasksData;
  }

  getTask(id) {
    return this.tasksData.find(t => { return t.id === Number(id); });
  }

  addTask(task) {
    const ids = this.tasksData.map(t => { return Number(t.id); });
    task.id = Math.max.apply(Math, ids) + 1;
    this.tasksData.push(task);

    this.save();
  }

  editTask(task) {
    this.deleteTask(task.id);
    this.tasksData.push(task);

    this.save();
  }

  deleteTask(id) {
    this.tasksData = this.tasksData.filter(t => { return t.id !== Number(id); });

    this.save();
  }

  save() {
    localStorage.setItem(this.storageKey, JSON.stringify(this.tasksData));
  }
}