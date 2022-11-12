import { TestBed } from '@angular/core/testing';

import { AvailableEventsResolver } from './available-events.resolver';

describe('AvailableEventsResolver', () => {
  let resolver: AvailableEventsResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(AvailableEventsResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
