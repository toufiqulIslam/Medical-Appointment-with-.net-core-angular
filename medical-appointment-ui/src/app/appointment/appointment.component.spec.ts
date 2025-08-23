import { ComponentFixture, TestBed } from '@angular/core/testing';

import { appointmentComponent } from './appointment.component';

describe('appointmentComponent', () => {
  let component: appointmentComponent;
  let fixture: ComponentFixture<appointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ appointmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(appointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
