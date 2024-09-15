import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllgenreComponent } from './allgenre.component';

describe('AllgenreComponent', () => {
  let component: AllgenreComponent;
  let fixture: ComponentFixture<AllgenreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AllgenreComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AllgenreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
