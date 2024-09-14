import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignupInputComponent } from './signup-input.component';

describe('SignupInputComponent', () => {
  let component: SignupInputComponent;
  let fixture: ComponentFixture<SignupInputComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SignupInputComponent]
    });
    fixture = TestBed.createComponent(SignupInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
