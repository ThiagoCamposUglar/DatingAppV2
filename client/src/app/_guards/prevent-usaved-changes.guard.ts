import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';
import { inject } from '@angular/core';
import { ComfirmService } from '../_services/comfirm.service';

export const preventUsavedChangesGuard: CanDeactivateFn<MemberEditComponent> = (component) => {
  const confirmService = inject(ComfirmService);

  if(component.editForm?.dirty){
    return confirmService.confirm('Confirmation', 'Are you sure you want to continue? Any unsaved changes will be lost');
  }

  return true;
};
