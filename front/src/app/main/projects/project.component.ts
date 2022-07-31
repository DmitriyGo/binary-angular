import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {ModalService} from "../../_modal";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {firstValueFrom, Observable} from "rxjs";
import {Project} from "../../Models/Project.model";
import {ProjectService} from "./project.service";
import {User} from "../../Models/User.model";
import {Team} from "../../Models/Team.model";
import {ProjTask} from "../../Models/ProjTask.model";

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {

  @Output() onPersonPut = new EventEmitter<Project>();
  form!: FormGroup;
  putForm!: FormGroup;

  _filter: string = '';

  _projects: Project[] = []

  _shownPersons: Project[] = []
  _shownPersonsCount: number = 10;

  _pagesCount!: number[];

  _pageNumber: number = 1

  constructor(private personService: ProjectService, private modalService: ModalService) {
  }

  openModal(id: string, person?: Project) {
    this.modalService.open(id);
    if (id == 'person-edit-modal') {
      this.putForm.value.name = person?.name
      console.log(this.putForm.controls['name'])
      if(person){
        this.putForm.controls['id'].setValue(person.id)
        this.putForm.controls['name'].setValue(person.name)
        this.putForm.controls['description'].setValue(person.description)
        this.putForm.controls['authorId'].setValue(person.authorId)
        this.putForm.controls['teamId'].setValue(person.teamId)
        this.putForm.controls['createdAt'].setValue(person.createdAt)
        this.putForm.controls['deadLine'].setValue(person.deadLine)

        console.log(this.putForm)
      }
    }
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

  ngOnInit() {
    this.loadPeople().subscribe(p => this._projects = p);

    this.form = new FormGroup({
      birthday: new FormControl('', Validators.required),
      id: new FormControl('', Validators.required),
      authorId: new FormControl('', Validators.required),
      teamId: new FormControl('', Validators.required),
      name: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      deadLine: new FormControl('', Validators.required),
      createdAt: new FormControl('', Validators.required),
    })

    this.putForm = new FormGroup({
      birthday: new FormControl('', Validators.required),
      id: new FormControl('', Validators.required),
      authorId: new FormControl('', Validators.required),
      teamId: new FormControl('', Validators.required),
      name: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      deadLine: new FormControl('', Validators.required),
      createdAt: new FormControl('', Validators.required),
    })
  }

  loadPeople(): Observable<Project[]> {
    return this.personService.getPage(this._pageNumber-1)
  }

  async postPerson() {
    this.personService.post({
      id: 0,
      name: this.form.value.name,
      authorId: this.form.value.authorId,
      description: this.form.value.description,
      teamId: this.form.value.teamId,
      createdAt: this.form.value.createdAt,
      deadLine: this.form.value.deadLine,
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
      authorId: this.putForm.value.authorId,
      description: this.putForm.value.description,
      teamId: this.putForm.value.teamId,
      createdAt: this.putForm.value.createdAt,
      deadLine: this.putForm.value.deadLine,
    }).subscribe(u => {
      this.loadPeople().subscribe(u => {
        this.form.reset()
        this.form.markAsUntouched()
        this._projects = u
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

    this.loadPeople().subscribe(r => this._projects = r)
  }

  removePerson(personId: number) {
    this.personService.delete(personId).subscribe(u => {
      this.loadPeople().subscribe(r => this._projects = r)
    })
  }

}
