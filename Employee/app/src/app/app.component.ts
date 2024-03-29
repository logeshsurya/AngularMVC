import { HttpClient } from '@angular/common/http';
import { Component, ViewChild } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { CellClickedEvent, ColDef, GridReadyEvent } from 'ag-grid-community';
import { Observable } from 'rxjs';
import 'ag-grid-enterprise';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';

 public rowData$! : Observable<any[]>


 public  colDefs:ColDef[]=[
    {field:'make'},
    {field:'model'},
    {field:'price'}
  ];


  public defaultColDef: ColDef = {
    sortable: true,
    filter: true,
  };

  @ViewChild(AgGridAngular) agGrid!: AgGridAngular;

  constructor(private http: HttpClient) {}

  onGridReady(params:GridReadyEvent)
  {
    this.rowData$ = this.http
        .get<any[]>('https://www.ag-grid.com/example-assets/row-data.json')
  }


 onCellClicked( e: CellClickedEvent): void {
  console.log('cellClicked', e);
}

clearSelection(): void {
  this.agGrid.api.deselectAll();
}

}
