import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignationProfileComponent } from './designation-profile.component';

describe('DesignationProfileComponent', () => {
  let component: DesignationProfileComponent;
  let fixture: ComponentFixture<DesignationProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DesignationProfileComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DesignationProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
