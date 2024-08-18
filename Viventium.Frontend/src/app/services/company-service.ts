import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { from, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private http: HttpClient) { }

  public importSimple(file: File) {
    const data = new FormData();
    data.append("fileData", file);
    return this.http.post(`${environment.serviceUrl}/dataStore`, data);
  }



  public importBinary(file: File) {
    var promise = new Promise((resolve, error) => {
      var data = file.stream();
      let bytesUploaded = 0;
      let totalBytes = file.size;
      const progressTrackingStream = new TransformStream({
        transform(chunk, controller) {
          controller.enqueue(chunk);
          bytesUploaded += chunk.byteLength;
          console.log("upload progress:", bytesUploaded / totalBytes);
        },
        flush(controller) {
          resolve(null);
          console.log("completed stream");
        },
      });

      var options: RequestInit = {
        body: data.pipeThrough(progressTrackingStream),
        headers: { 'Content-Type': 'text/plain' },
        method: 'POST'
      };
      var o: any = options;
      o.duplex = "half";

      fetch(`${environment.serviceUrl}/dataStore/stream`, o).catch(e => {
        error(e);
      });
    });

    return from(promise);
    //return this.http.post(`${environment.serviceUrl}/dataStore/stream`, data, {du});
  }
}
