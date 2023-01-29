import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-image',
  templateUrl: './image.component.html',
  styleUrls: ['./image.component.css']
})
export class ImageComponent {
  @Input() type: string = ''
  @Input() id: any = '0'
  @Input() class: string = ''
  apiUrl = 'https:localhost:5001/api/image'

  url = () => `${this.apiUrl}/${this.type}/${this.id}`
}
