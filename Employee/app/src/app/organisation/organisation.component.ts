
import { Component,OnInit} from '@angular/core';
import { EmployeeService } from 'src/app/employee.service';
import { PaginationService } from 'src/app/pagination.service';
import { ColDef,GridApi,ColumnApi, Grid, CellClickedEvent, GetRowIdFunc, GetRowIdParams, RowNode,ValueGetterParams,ValueSetterParams, Module, ClientSideRowModelSteps } from 'ag-grid-community';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import {ICellRendererAngularComp} from 'ag-grid-angular';
import {ICellRendererParams} from "ag-grid-community";
import {ClientSideRowModelModule } from "@ag-grid-community/client-side-row-model";
import {RangeSelectionModule} from "@ag-grid-enterprise/range-selection"

function actionCellRenderer(params:any)
{
  let eGui = document.createElement("div");

  let editingCells = params.api.getEditingCells();

  let isCurrentRowEditing = editingCells.some((cell:any) => 
  {
    return cell.rowIndex == params.node.rowIndex;
  }
  );

  if (isCurrentRowEditing)
  {
    eGui.innerHTML = `
    <button  class="col-auto btn btn-sm btn-success button"  data-action="update"> update  </button>
    <button  class="col-auto btn btn-sm btn-light button"  data-action="cancel" > cancel </button>`;
  } 
  else
  {
  eGui.innerHTML = `
  <button  class="col-auto btn btn-sm btn-info button"  data-action="edit" > edit  </button>
  <button  class="col-auto btn btn-sm btn-danger button" data-action = "delete"> delete </button>`;
  }

return eGui;

}

@Component({
  selector: 'app-employees',
  templateUrl: './organisation.component.html',
  styleUrls: ['./organisation.component.css'],
  })
export class OrganisationComponent implements OnInit {
  endpoint = 'Organisation';
  id: any;
  public api!: GridApi;
  public columnApi!: ColumnApi;

  // public modules:Module[]= [ClientSideRowModelModule,RangeSelectionModule]
  public columnDefs: ColDef[];
  public defaultColDef: any;
  public rowData:any[]=[];

  constructor
  (
    private employeeService: EmployeeService,
    private pagination: PaginationService,
    private router: Router,
    private toastr:ToastrService
  ) 
  {
    this.columnDefs = [
      {
        headerName:'Organisation Id',
        field:'id',
      },
      {
        headerName:'Organisation Name',
        field:'organisation_Name',
      },
      {
        headerName:"Actions",
        minWidth:150,
        cellRenderer:actionCellRenderer,
        editable:false,
        colId:"action"
      }

    ];
    this.defaultColDef = {
      filter:'agTextColumnFilter',
        enableSorting:true,
        editable:true,
        sortable:true,
        resizable:true
    }; 
  }

  ngOnInit(): void
   {
   }

  onGridReady(params:any)
  {
    this.api = params.api;
    this.columnApi = params.columnApi;
    this.api.sizeColumnsToFit();
    // params.columnApi.setColumnsVisible(true);
    // params.columnApi.setColumnsVisible(false);
    this.employeeService.GetAll(this.endpoint).subscribe((data) => {
    this.rowData = data;
    console.log(data);
    });
  }
  
  onCellClicked (params:any){

    if (params.column.colId === "action" && params.event.target.dataset.action)
    {
      let action = params.event.target.dataset.action;
    
        if(action === "edit")
        {
          params.api.startEditingCell(
            {
              // rowIndex:params.node.rowIndex,
              // colKey:params.columnApi.getDisplayedCenterColumns()[0].colId
              // this.id   = params.node.data.id,
              // this.router.navigateByUrl('../edit-employee',this.id)
            },
            this.id = params.node.data.id,
            console.log(this.id),
            this.router.navigateByUrl(`/edit-organisation/${this.id}`)
          );
        }

        if(action === "delete")
        {
          // params.api.applyTransaction(
          //   {
          //     remove:[params.node.data]
          //   }
          // );
          params.api.startEditingCell(
            {
              // rowIndex:params.node.rowIndex,
              // colKey:params.columnApi.getDisplayedCenterColumns()[0].colId
              // this.id   = params.node.data.id,
              // this.router.navigateByUrl('../edit-employee',this.id)
            },
            this.id = params.node.data.id,
            console.log(this.id),

            this.employeeService.Delete(this.endpoint, this.id).subscribe((result) => {
            console.log(result);
            this.ngOnInit();
          })
         
          );
        }

        if (action === "update") {
          params.api.stopEditing(false);
        }
  
        if (action === "cancel") {
          params.api.stopEditing(true);
        }
  }
}

onRowEditingStarted(params:any)
{
  params.api.refreshCells(
    {
      columns:["action"],
      rowNodes:[params.node],
      force:true
    }
  );
}

onRowEditingStopped(params:any) 
  {
    params.api.refreshCells({
      columns: ["action"],
      rowNodes: [params.node],
      force: true
    });
  }

clearSelection()
  {
    this.api.deselectAll();
  }
}









// import { Component, OnInit } from '@angular/core';
// import { MatDialog } from '@angular/material/dialog';
// import { Router } from '@angular/router';
// import { EmployeeService } from '../employee.service';


// @Component({
//   selector: 'app-organisation',
//   templateUrl: './organisation.component.html',
//   styleUrls: ['./organisation.component.css']
// })
// export class OrganisationComponent implements OnInit {
//   endpoint = "Organisation";
//   totalLength: any;
//   page: number = 1;
//   searchValue = '';
//   public filteredData: any[] = [];
//   organisationname: any;
//   data:any;


//   constructor(private employeeService:EmployeeService) { }

//   ngOnInit(): void {
//     this.employeeService.GetAll(this.endpoint).subscribe(data => {
//       this.data = data;
//       this.filteredData = data;
//       this.totalLength = data;
//       console.log(data);
//     });

//   }



//   deleteOrganisation(id:any)
//   {
//     this.employeeService.Delete(this.endpoint,id).subscribe((result)=>
//     {
//       console.log(result);
//       this.ngOnInit();
//     })
//   }

//   Search(value: string) {
//     this.data = this.filteredData.filter(item =>
//       item.organisationName.toLowerCase().includes(value.toLowerCase()))
//     this.page = 1;
//   }
// }
