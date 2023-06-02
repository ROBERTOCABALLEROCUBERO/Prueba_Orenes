import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccesoVehiculoComponent } from './acceso-vehiculo.component';

describe('AccesoVehiculoComponent', () => {
  let component: AccesoVehiculoComponent;
  let fixture: ComponentFixture<AccesoVehiculoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccesoVehiculoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccesoVehiculoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
