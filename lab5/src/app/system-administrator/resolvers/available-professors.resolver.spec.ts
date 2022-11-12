import { TestBed } from '@angular/core/testing';

import { AvailableProfessorsResolver } from './available-professors.resolver';

describe('AvailableProfessorsResolver', () => {
  let resolver: AvailableProfessorsResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(AvailableProfessorsResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
