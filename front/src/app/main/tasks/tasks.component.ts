import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {ModalService} from "../../_modal";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {firstValueFrom, Observable} from "rxjs";
import {TaskService} from "./tasks.service";
import {User} from "../../Models/User.model";
import {Team} from "../../Models/Team.model";
import {ProjTask} from "../../Models/ProjTask.model";

@Component({
  selector: 'app-project',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss']
})
export class TasksComponent implements OnInit {

  @Output() onPersonPut = new EventEmitter<ProjTask>();
  form!: FormGroup;
  putForm!: FormGroup;

  _filter: string = '';

  _tasks: ProjTask[] = []

  _shownTasks: ProjTask[] = []
  _shownTasksCount: number = 10;

  _pagesCount!: number[];

  _pageNumber: number = 1

  constructor(private personService: TaskService, private modalService: ModalService) {
  }

  openModal(id: string, person?: ProjTask) {
    this.modalService.open(id);
    if (id == 'person-edit-modal') {
      this.putForm.value.name = person?.name
      if(person){
        this.putForm.controls['id'].setValue(person.id)
        this.putForm.controls['name'].setValue(person.name)
        this.putForm.controls['state'].setValue(person.state)
        this.putForm.controls['description'].setValue(person.description)
        this.putForm.controls['performerId'].setValue(person.performerId)
        this.putForm.controls['projectId'].setValue(person.projectId)
        this.putForm.controls['createdAt'].setValue(person.createdAt)
        this.putForm.controls['finishedAt'].setValue(person.finishedAt)

      }
    }
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

  ngOnInit() {
    this.loadPeople().subscribe(p => this._tasks = p);

    this.form = new FormGroup({
      id: new FormControl('', Validators.required),
      state: new FormControl('', Validators.required),
      performerId: new FormControl('', Validators.required),
      projectId: new FormControl('', Validators.required),
      name: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      finishedAt: new FormControl('', Validators.required),
      createdAt: new FormControl('', Validators.required),
    })

    this.putForm = new FormGroup({
      id: new FormControl('', Validators.required),
      performerId: new FormControl('', Validators.required),
      projectId: new FormControl('', Validators.required),
      name: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      createdAt: new FormControl('', Validators.required),
      finishedAt: new FormControl('', Validators.required),
      state: new FormControl('', Validators.required),
    })
  }

  loadPeople(): Observable<ProjTask[]> {
    return this.personService.getPage(this._pageNumber-1)
  }

  async postPerson() {
    this.personService.post({
      id: 0,
      name: this.form.value.name,
      projectId: this.form.value.projectId,
      performerId: this.form.value.performerId,
      description: this.form.value.description,
      state: this.form.value.state,
      createdAt: this.form.value.createdAt,
      finishedAt: this.form.value.finishedAt,
    }).subscribe(u => {
      this.loadPeople().subscribe(u => {
        this.form.reset()
        this.form.markAsUntouched()
      });
    })
  }

  async putPerson() {
    this.personService.put(this.putForm.value.id, {
      id: this.putForm.value.id,
      name: this.putForm.value.name,
      projectId: this.putForm.value.projectId,
      performerId: this.putForm.value.performerId,
      description: this.putForm.value.description,
      state: this.putForm.value.state,
      createdAt: this.putForm.value.createdAt,
      finishedAt: this.putForm.value.finishedAt,
    }).subscribe(u => {
      this.loadPeople().subscribe(u => {
        this.form.reset()
        this.form.markAsUntouched()
        this._tasks = u
      });
    })
  }

  changePage(inc: number) {
    if(inc == -1 && this._pageNumber > 1)
      --this._pageNumber
    else if(inc == +1)
      ++this._pageNumber
    else
      return

    this.loadPeople().subscribe(r => this._tasks = r)
  }

  removePerson(personId: number) {
    this.personService.delete(personId).subscribe(u => {
      this.loadPeople()
    })
  }

}
