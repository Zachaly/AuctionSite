import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserListsPageComponent } from './user-lists-page.component';

describe('UserListsPageComponent', () => {
  let component: UserListsPageComponent;
  let fixture: ComponentFixture<UserListsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserListsPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserListsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
