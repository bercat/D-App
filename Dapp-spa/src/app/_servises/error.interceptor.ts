import { Inject, Injectable } from '@angular/core';
import { HttpInterceptor, HttpErrorResponse, HttpRequest, HttpHandler, HttpEvent, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError(error => {
                if (error instanceof HttpErrorResponse) {
                    if (error.status === 401) {
                        return throwError(error.statusText);
                    }
                    const applicationError = error. headers.get('Application-Error');
                    if (applicationError)  {
                        console.error(applicationError);
                        return throwError(applicationError);
                    }
                    /* om ikke modalStateErrors blir det inni serverError*/
                    const  serverError  =  error.error.errors || error.error;
                    let modalStateErrors = ''; /*setter modalStateErrors inni denne variablen*/
                    if (serverError && typeof serverError === 'object') {
                        for (const key in serverError) {
                            if (serverError[key]) {
                                modalStateErrors += serverError[key] + '\n';
                            }
                        }
                    }
                    return throwError(modalStateErrors || serverError || 'Server Error');
                }
            })
        );
    }
}


export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
};


