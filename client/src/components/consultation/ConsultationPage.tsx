import React from 'react';
import { RouteComponentProps, withRouter } from 'react-router-dom';
import style from './consultation.scss';
import { ConsultPart } from '../../helpers/utils';
import * as consultActions from '../../stores/consultations/consultationActions';
import { ApplicationState } from '../../stores';
import { compose } from 'redux';
import { connect, ConnectedProps } from 'react-redux';
import PatientInfo from '../common/PatientInfo';
import CasefileInfo from '../common/CasefileInfo';
import TreatmentsAndPlanForm from './TreatmentsAndPlanForm';
import SubjectiveForm from './SubjectiveForm';
import ObjectiveForm from './ObjectiveForm';
import moment from 'moment';
import { DatePicker, Button, Modal } from 'antd';
import { DeleteOutlined, ExclamationCircleOutlined } from '@ant-design/icons';
import * as G from '../../api/generated';

const mapStateToProps = (state: ApplicationState) => state.consultation;
const connector = connect(mapStateToProps, consultActions);

type Props = ConnectedProps<typeof connector> &
  RouteComponentProps<{ patientId: string; casefileId: string; consultId: string }>;

type State = {
  display: ConsultPart;
  isNewConsult: boolean;
};

class ConsultationPage extends React.Component<Props, State> {
  state = {
    display: ConsultPart.Subjective,
    isNewConsult: this.props.match.params.consultId === 'new'
  };

  componentDidMount() {
    this.state.isNewConsult ? this.props.clearConsult() : this.ensureDataFetched();
  }

  ensureDataFetched = () => {
    const { id, match, getConsult } = this.props;
    const cId = parseInt(match.params.consultId);

    if (id !== cId) getConsult(cId);
  };

  onSubmit = () => {
    const { id, date, practitionerId, match } = this.props;
    const { subjectiveAssessment, objectiveAssessment, treatments, plans } = this.props;
    const casefileId = parseInt(match.params.casefileId);

    this.state.isNewConsult
      ? this.props.createConsult({
          subjectiveAssessment,
          objectiveAssessment,
          treatments,
          plans,
          casefileId,
          practitionerId: 1,
          date: date ? date : moment().format()
        })
      : this.props.updateConsult(id, {
          id,
          casefileId,
          date,
          practitionerId,
          subjectiveAssessment,
          objectiveAssessment,
          treatments,
          plans
        });
  };

  selectSection = (display: ConsultPart) => this.setState({ display });

  renderConsultSection = (consultPart: ConsultPart) => {
    const { subjectiveAssessment, objectiveAssessment, treatments, plans, id } = this.props;

    switch (consultPart) {
      case ConsultPart.Subjective:
        const data = this.state.isNewConsult && id !== 0 ? undefined : subjectiveAssessment;
        return (
          <SubjectiveForm
            data={data}
            display={consultPart}
            changeSection={this.selectSection}
            saveValues={this.modifySubjective}
          />
        );
      case ConsultPart.Objective:
        return (
          <ObjectiveForm
            data={objectiveAssessment}
            display={consultPart}
            changeSection={this.selectSection}
            saveValues={this.modifyObjective}
          />
        );
      default:
        return (
          <TreatmentsAndPlanForm
            data={{ treatments, plans }}
            display={consultPart}
            changeSection={this.selectSection}
            saveValues={this.props.modifyTreatmentsAndPlans}
            createConsult={this.onSubmit}
          />
        );
    }
  };

  modifySubjective = (subjective: G.IGetSubjectiveAssessmentVm) => {
    const { id, consultationId } = this.props.subjectiveAssessment;
    return this.props.modifySubjective({ ...subjective, id, consultationId });
  };

  modifyObjective = (subjective: G.IGetObjectiveAssessmentVm) => {
    const { id, consultationId } = this.props.objectiveAssessment;
    return this.props.modifyObjective({ ...subjective, id, consultationId });
  };

  changeDate = (date: moment.Moment | null) => date && this.props.modifyDate(date.format());

  getDate = (date: string) =>
    !date && this.state.isNewConsult ? moment() : !date ? undefined : moment(date);

  showDelete = () => {
    const { deleteConsult, id } = this.props;

    Modal.confirm({
      title: 'Are you sure?',
      icon: <ExclamationCircleOutlined />,
      content: 'Deleted consultations cannot be recovered!',
      okText: 'Delete',
      okType: 'danger',
      onOk: () => deleteConsult(id)
    });
  };

  render() {
    const { display, isNewConsult } = this.state;
    const { date } = this.props;

    return (
      <>
        <PatientInfo />
        <CasefileInfo />
        <div className={isNewConsult ? style.dateRow : style.dateRowSpaced}>
          <div>
            <label className={style.consultDateLabel}>Consult date:</label>
            <DatePicker format="DD-MM-YYYY" onChange={this.changeDate} value={this.getDate(date)} />
          </div>
          {!isNewConsult && <Button danger icon={<DeleteOutlined />} onClick={this.showDelete} />}
        </div>
        {this.renderConsultSection(display)}
      </>
    );
  }
}

export default compose<React.ComponentType>(withRouter, connector)(ConsultationPage);
