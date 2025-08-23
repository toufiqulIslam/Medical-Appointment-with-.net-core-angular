import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowappointmentComponent } from './show-appointment.component';

describe('ShowappointmentComponent', () => {
  let component: ShowappointmentComponent;
  let fixture: ComponentFixture<ShowappointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowappointmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowappointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
