import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditappointmentComponent } from './add-edit-appointment.component';

describe('AddEditappointmentComponent', () => {
  let component: AddEditappointmentComponent;
  let fixture: ComponentFixture<AddEditappointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditappointmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditappointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
