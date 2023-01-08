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
  <button  class="col-auto btn btn-sm btn-success button"  data-action="edit" > Edit  </button>
  <button  class="col-auto btn btn-sm btn-info button" data-action = "details"> Details </button>
  <button  class="col-auto btn btn-sm btn-danger button" data-action = "delete"> Delete </button>`;
  }

return eGui;

}

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css'],
  })
export class EmployeesComponent implements OnInit {
  endpoint = 'Employee';
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
        headerName:'Employee Id',
        field:'id',
        maxWidth:150
      },
      {
        headerName:'First Name',
        field:'firstname',
      },
      {
        headerName:'Last Name',
        field:'lastname',
      },
      {
        headerName:'Department Name',
        field:'department_Name',
      },
      {
        headerName:'Designation Name',
        field:'designation_Name',
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
            this.router.navigateByUrl(`/edit-employee/${this.id}`)
          );
        }
        if(action === "details")
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
            this.router.navigateByUrl(`/employee-profile/${this.id}`)
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
            this.api.refreshCells(params);
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
onFilterTextBoxChanged() {
  this.api.setQuickFilter(
    (document.getElementById('filter-text-box') as HTMLInputElement).value
  );
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



  

  

  





















  // this.api.setDomLayout('autoHeight');

// editEmployee()
// {
//   debugger;
//   const d = this.api.getEditingCells();
//   if(this.api.getSelectedRows().length == 0)
//   {
//     this.toastr.error("error","Select the Employee to edit");
//     return;
//   }
//   var row = this.api.getSelectedRows();
//   this.employeeService.Edit(this.endpoint,row[0]).subscribe((data)=>
//     {
//       this.toastr.success("success");
//       this.ngOnInit();
//     })

// }

//pagination
    // this.pageNumber[0] ;
    // this.pagination.temppage = 0;

     // this.getallemployeescount();


  // page: number = 1;
  // searchValue = '';
  
  // public filterdata: any[] = [];
  // length: any;

  // pageNo: any = 1;
  // public data: any[] = [];
  // pageNumber: boolean[] = [];
  // sortOrder: any = 'firstname';

  // pageField: any[] = [];
  // exactPageList: any;
  // paginationData: any;
  // employeesPerPage: any = 5;
  // totalEmployees: any;
  // totalEmployeesCount: any;

  // deleteEmployee(id: any) {
    // this.employeeService.Delete(this.endpoint, id).subscribe((result) => {
    //   console.log(result);
    //   this.ngOnInit();
    // });
  // }

  // Search(value: string) {
  //   this.data = this.filterdata.filter(
  //     (item) =>
  //       item.firstname.toLowerCase().includes(value.toLowerCase()) ||
  //       item.lastname.toLowerCase().includes(value.toLowerCase())
  //   );

  //   console.log(this.data);

  //   this.page = 1;
  // }

  // totalNoOfPages() {
  //   this.paginationData = Number(
  //     this.totalEmployeesCount / this.employeesPerPage
  //   );
  //   let tempPageData = this.paginationData.toFixed();
  //   if (Number(tempPageData) < this.paginationData) {
  //     this.exactPageList = Number(tempPageData) + 1;
  //     this.pagination.exactPageList = this.exactPageList;
  //   } else {
  //     this.exactPageList = Number(tempPageData);
  //     this.pagination.exactPageList = this.exactPageList;
  //   }
  //   this.pagination.pageOnLoad();
  //   this.pageField = this.pagination.pageField;
  // }

  // showEmployeesByPageNumber(page: any, i: number) {
  //   this.data = [];
  //   this.pageNumber = [];
  //   this.pageNumber[i] = true;
  //   this.pageNo = page;
  //   this.Getallemployees();
  // }

//   getallemployeescount() {
//     this.employeeService.count().subscribe((res: any) => {
//       this.totalEmployeesCount = res;
//       this.totalNoOfPages();
//     });
//   }
// }

//   Onchange() {
//     const item = document.getElementById('Category_Name') as HTMLSelectElement;

//     this.productservice.getproductfilters(item.value).subscribe((res) => {
//       console.warn(res);

//       this.Product = res;
//     });

//     console.warn(item.value);
//   }
// }



    // this.employeeService.GetAllEmployee(this.endpoint).subscribe((data)=>
    // {
    //   this.data = data;
    //   this.filterdata = data;
    //   this.length = data.length;
    //   this.getallemployeescount();
    //   console.log(data);
    // });