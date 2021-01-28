import { TestBed } from '@angular/core/testing';

import { JWTInterceptorService } from './jwt-interceptor.service';

describe('JwtInterceptorService', () => {
  let service: JWTInterceptorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(JWTInterceptorService);
  });

});
