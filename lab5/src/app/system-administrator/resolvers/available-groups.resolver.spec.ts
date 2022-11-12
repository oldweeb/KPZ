import { TestBed } from '@angular/core/testing';

import { AvailableGroupsResolver } from './available-groups.resolver';

describe('AvailableGroupsResolver', () => {
  let resolver: AvailableGroupsResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(AvailableGroupsResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
