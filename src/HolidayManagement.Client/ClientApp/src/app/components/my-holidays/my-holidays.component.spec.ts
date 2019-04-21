import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyHolidaysComponent } from './my-holidays.component';

describe('MyHolidaysComponent', () => {
  let component: MyHolidaysComponent;
  let fixture: ComponentFixture<MyHolidaysComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyHolidaysComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyHolidaysComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
