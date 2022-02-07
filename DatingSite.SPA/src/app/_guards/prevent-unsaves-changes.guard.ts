import { CanDeactivate } from '@angular/router';
import { UserEditComponent } from '../users/user-edit/user-edit.component';

export class PreventUnsavesChanges implements CanDeactivate<UserEditComponent> {
    canDeactivate(component: UserEditComponent) {
        if (component.editForm.dirty) {
            return confirm('Are you sure you want to continue? Every unsaved changes will be lost');
        }
        return true;
    }
}
