import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {ModalService} from "../../_modal";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Observable} from "rxjs";
import {TeamsService} from "./teams.service";
import {Team} from "../../Models/Team.model";

@Component({
  selector: 'app-project', templateUrl: './teams.component.html', styleUrls: ['./teams.component.scss']
})
export class TeamsComponent implements OnInit {

  @Output() onPersonPut = new EventEmitter<Team>();
  form!: FormGroup;
  putForm!: FormGroup;

  _filter: string = '';

  _teams: Team[] = [];

  _pagesCount!: number[];

  _pageNumber: number = 1

  constructor(private personService: TeamsService, private modalService: ModalService) {
  }

  openModal(id: string, person?: Team) {
    this.modalService.open(id);
    if (id == 'person-edit-modal') {
      this.putForm.value.name = person?.name
      if (person) {
        this.putForm.controls['id'].setValue(person.id)
        this.putForm.controls['name'].setValue(person.name)
        this.putForm.controls['createdAt'].setValue(person.createdAt)

      }
    }
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

  ngOnInit() {
    this.loadPeople().subscribe(p => this._teams = p);

    this.form = new FormGroup({
      id: new FormControl('', Validators.required),
      name: new FormControl('', Validators.required),
      createdAt: new FormControl('', Validators.required),
    })

    this.putForm = new FormGroup({
      id: new FormControl('', Validators.required),
      name: new FormControl('', Validators.required),
      createdAt: new FormControl('', Validators.required)
    })
  }

  loadPeople(): Observable<Team[]> {
    return this.personService.getPage(this._pageNumber - 1)
  }

  async postPerson() {
    this.personService.post({
      id: 0, name: this.form.value.name, createdAt: this.form.value.createdAt,
    }).subscribe(u => {
      this.loadPeople().subscribe(u => {
        this.form.reset()
        this.form.markAsUntouched()
      });
    })
  }

  async putPerson() {
    this.personService.put(this.putForm.value.id, {
      id: this.putForm.value.id, name: this.putForm.value.name, createdAt: this.putForm.value.createdAt,
    }).subscribe(u => {
      this.loadPeople().subscribe(u => {
        this.form.reset()
        this.form.markAsUntouched()
        this._teams = u
      });
    })
  }

  changePage(inc: number) {
    if (inc == -1 && this._pageNumber > 1)
      --this._pageNumber
    else if (inc == +1)
      ++this._pageNumber
    else
      return

    this.loadPeople().subscribe(r => this._teams = r)
  }

  removePerson(personId: number) {
    this.personService.delete(personId).subscribe(u => {
      this.loadPeople()
    })
  }

}
