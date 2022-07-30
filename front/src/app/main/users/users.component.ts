import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {ModalService} from "../../_modal";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Observable} from "rxjs";
import {UsersService} from "./users.service";
import {User} from "../../Models/User.model";

@Component({
  selector: 'app-project', templateUrl: './users.component.html', styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  @Output() onPersonPut = new EventEmitter<User>();
  form!: FormGroup;
  putForm!: FormGroup;

  _filter: string = '';

  _users: User[] = [];

  _pagesCount!: number[];

  _pageNumber: number = 1

  constructor(private personService: UsersService, private modalService: ModalService) {
  }

  openModal(id: string, person?: User) {
    this.modalService.open(id);
    if (id == 'person-edit-modal') {
      if (person) {
        this.putForm.controls['id'].setValue(person.id)
        this.putForm.controls['firstName'].setValue(person.firstName)
        this.putForm.controls['lastName'].setValue(person.lastName)
        this.putForm.controls['email'].setValue(person.email)
        this.putForm.controls['birthDay'].setValue(person.birthDay)
        this.putForm.controls['registeredAt'].setValue(person.registeredAt)

      }
    }
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

  ngOnInit() {
    this.loadPeople().subscribe(p => this._users = p);

    this.form = new FormGroup({
      id: new FormControl('', Validators.required),
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
      registeredAt: new FormControl('', Validators.required),
      birthDay: new FormControl('', Validators.required),
    })

    this.putForm = new FormGroup({
      id: new FormControl('', Validators.required),
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
      registeredAt: new FormControl('', Validators.required),
      birthDay: new FormControl('', Validators.required),
    })
  }

  loadPeople(): Observable<User[]> {
    return this.personService.getPage(this._pageNumber - 1)
  }

  async postPerson() {
    this.personService.post({
      id: 0,
      firstName: this.form.value.firstName,
      lastName: this.form.value.lastName,
      email: this.form.value.email,
      birthDay: this.form.value.birthDay,
      teamId: this.form.value.teamId,
      registeredAt: this.form.value.registeredAt,
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
      firstName: this.putForm.value.firstName,
      lastName: this.putForm.value.lastName,
      email: this.putForm.value.email,
      birthDay: this.putForm.value.birthDay,
      teamId: this.putForm.value.teamId,
      registeredAt: this.putForm.value.registeredAt,
    }).subscribe(u => {
      this.loadPeople().subscribe(u => {
        this.form.reset()
        this.form.markAsUntouched()
        this._users = u
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

    this.loadPeople().subscribe(r => this._users = r)
  }

  removePerson(personId: number) {
    this.personService.delete(personId).subscribe(u => {
      this.loadPeople()
    })
  }

}
