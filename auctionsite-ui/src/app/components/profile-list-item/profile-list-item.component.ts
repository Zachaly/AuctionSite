import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-profile-list-item',
  templateUrl: './profile-list-item.component.html',
  styleUrls: ['./profile-list-item.component.css']
})
export class ProfileListItemComponent {
  @Input() name: string = ''
  @Input() value: any
}
