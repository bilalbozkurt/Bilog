import { TestBed } from '@angular/core/testing';

import { HashtagPageService } from './hashtag-page.service';

describe('HashtagPageService', () => {
  let service: HashtagPageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HashtagPageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
