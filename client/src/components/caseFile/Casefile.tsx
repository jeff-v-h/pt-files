import React from 'react';
import { connect, ConnectedProps } from 'react-redux';
import { compose } from 'redux';
import { RouteComponentProps, withRouter } from 'react-router-dom';
import * as casefileActions from '../../stores/casefiles/casefileActions';
import { ApplicationState } from '../../stores';
import CasefileForm from './CasefileForm';
import { CasefileBase } from 'src/models/casefileModels';
import PatientInfo from '../common/PatientInfo';
import CasefileInfo from '../common/CasefileInfo';
import style from './casefile.scss';
import { Button, Modal } from 'antd';
import { DeleteOutlined, ExclamationCircleOutlined } from '@ant-design/icons';

const mapStateToProps = (state: ApplicationState) => state.casefile;
const connector = connect(mapStateToProps, casefileActions);

type Props = ConnectedProps<typeof connector> &
  RouteComponentProps<{ patientId: string; casefileId: string }>;
type State = { isNew: boolean };

class Casefile extends React.Component<Props, State> {
  state = {
    isNew: this.props.match.params.casefileId === 'new'
  };

  onSubmit = async (values: CasefileBase) => {
    const { match, createCasefile, updateCasefile } = this.props;
    const casefileId = parseInt(match.params.casefileId);
    values.patientId = parseInt(match.params.patientId);
    this.state.isNew
      ? createCasefile(values)
      : updateCasefile(casefileId, { ...values, id: casefileId });
  };

  showDelete = () => {
    const { deleteCasefile, match } = this.props;
    const casefileId = parseInt(match.params.casefileId);

    Modal.confirm({
      title: 'Are you sure?',
      icon: <ExclamationCircleOutlined />,
      content: 'Deleting casefile will also remove all related consultations!',
      okText: 'Delete',
      okType: 'danger',
      onOk: () => deleteCasefile(casefileId)
    });
  };

  render() {
    const { isNew } = this.state;
    return (
      <>
        <PatientInfo />
        {!isNew && <CasefileInfo />}
        <div className={style.casefileSection}>
          {!isNew && (
            <Button
              danger
              icon={<DeleteOutlined />}
              onClick={this.showDelete}
              style={{ float: 'right' }}
            />
          )}
          <CasefileForm onSubmit={this.onSubmit} isSaving={this.props.isFetching} isNew={isNew} />
        </div>
      </>
    );
  }
}

export default compose<React.ComponentType>(withRouter, connector)(Casefile);
