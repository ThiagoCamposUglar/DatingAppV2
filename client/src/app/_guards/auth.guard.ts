import { inject, Injectable } from '@angular/core';

import { map, Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { CanActivateFn } from '@angular/router';


export const AuthGuard: CanActivateFn = (route, state) => {
  //os gurds automaticamente subscribe e unsubscribe um observable
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);

  return accountService.currentUser$.pipe(
    map(user => {
      if(user) return true;
      else{
        toastr.error('You shall not pass!');
        return false;
      }
    })
  );
}