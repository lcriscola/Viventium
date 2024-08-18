import { Component } from '@angular/core';
import { CompanyService } from '../services/company-service';
 

@Component({
  selector: 'app-demo',
  templateUrl: './demo.component.html',
  styleUrl: './demo.component.scss'
})
export class DemoComponent {

  constructor(private service: CompanyService) {

  }
  uploadSimple() {
    console.log(this.fileSimple);
    this.errors = undefined;
    if (this.fileSimple) {
      this.service.importSimple(this.fileSimple).subscribe(
        {
          next: x => {
            alert("Upload OK");
          },
          error: error => {
            if (error.status == "400" && error.error instanceof Array ) {
              this.displayErrors(error.error);
            }
            else {
              alert(JSON.stringify(error));
            }
          }
        }
      );
    }
  }

  errors?: string[];
  displayErrors(errors: string[]) {
    this.errors = errors;
  }
  uploadBinary() {
    this.errors = undefined;

    if (this.fileBinary) {
      this.service.importBinary(this.fileBinary).subscribe(
        {
          next: x => {
            alert("Upload OK");
          },
          error: error => {
            if (error.status == "400" && error.error instanceof Array) {
              this.displayErrors(error.error);
            }
            else {
              alert(JSON.stringify(error));
            }
          }
        }
      );
    }

  }

  fileSimple?: File|null;
  fileBinary?: File | null;
}
