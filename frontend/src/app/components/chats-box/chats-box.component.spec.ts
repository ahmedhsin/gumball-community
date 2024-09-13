import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatsBoxComponent } from './chats-box.component';

describe('ChatsBoxComponent', () => {
  let component: ChatsBoxComponent;
  let fixture: ComponentFixture<ChatsBoxComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ChatsBoxComponent]
    });
    fixture = TestBed.createComponent(ChatsBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
