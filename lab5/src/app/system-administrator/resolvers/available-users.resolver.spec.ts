import { TestBed } from '@angular/core/testing';

import { AvailableUsersResolver } from './available-users.resolver';

describe('AvailableUsersResolver', () => {
  let resolver: AvailableUsersResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(AvailableUsersResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
